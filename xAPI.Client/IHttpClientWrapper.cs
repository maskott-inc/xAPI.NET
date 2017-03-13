using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using xAPI.Client.Authenticators;
using xAPI.Client.Configuration;
using xAPI.Client.Exceptions;
using xAPI.Client.Resources;

namespace xAPI.Client
{
    internal interface IHttpClientWrapper
    {
        Task<T> GetJson<T>(string url, GetJsonOptions options);
        Task PutJson<T>(string url, PutJsonOptions options, T content);
        Task PostJson<T>(string url, PostJsonOptions options, T content);

        Task GetJsonDocument<T>(string url, GetJsonDocumentOptions options, BaseDocument<T> document);
        Task PutJsonDocument<T>(string url, PutJsonDocumentOptions options, BaseDocument<T> document);
        Task PostJsonDocument<T>(string url, PostJsonDocumentOptions options, BaseDocument<T> document);

        Task Delete(string url, DeleteOptions options);
    }

    internal class HttpClientWrapper : IHttpClientWrapper, IDisposable
    {
        private HttpClient _httpClient { get; set; }
        private ILRSAuthenticator _authenticator { get; set; }

        #region IHttpClientWrapper members

        async Task<T> IHttpClientWrapper.GetJson<T>(string url, GetJsonOptions options)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();
            await this.SetAuthorizationHeader();
            this.SetAcceptJsonContentType();
            this.SetAcceptedLanguages(options.AcceptedLanguages);

            // Perform request
            HttpResponseMessage response = await this._httpClient.GetAsync(url);
            this.EnsureResponseIsValid(response);

            // Parse content
            return await response.Content.ReadAsAsync<T>();
        }

        async Task IHttpClientWrapper.PutJson<T>(string url, PutJsonOptions options, T content)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();
            await this.SetAuthorizationHeader();

            // Perform request
            HttpResponseMessage response = await this._httpClient.PutAsJsonAsync(url, content);
            this.EnsureResponseIsValid(response);
        }

        async Task IHttpClientWrapper.PostJson<T>(string url, PostJsonOptions options, T content)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();
            await this.SetAuthorizationHeader();

            // Perform request
            HttpResponseMessage response = await this._httpClient.PostAsJsonAsync(url, content);
            this.EnsureResponseIsValid(response);
        }

        async Task IHttpClientWrapper.GetJsonDocument<T>(string url, GetJsonDocumentOptions options, BaseDocument<T> document)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();
            await this.SetAuthorizationHeader();
            this.SetAcceptJsonContentType();

            // Perform request
            HttpResponseMessage response = await this._httpClient.GetAsync(url);
            this.EnsureResponseIsValid(response);

            document.LastModified = response.Content.Headers.LastModified;
            document.ETag = response.Headers.ETag?.Tag;
            document.Content = await response.Content.ReadAsAsync<T>();
        }

        async Task IHttpClientWrapper.PutJsonDocument<T>(string url, PutJsonDocumentOptions options, BaseDocument<T> document)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();
            await this.SetAuthorizationHeader();
            this.SetIfMatchHeader(document.ETag);

            // Perform request
            HttpResponseMessage response = await this._httpClient.PutAsJsonAsync(url, document.Content);
            this.EnsureResponseIsValid(response);

            document.LastModified = response.Content?.Headers.LastModified ?? document.LastModified;
            document.ETag = response.Headers.ETag?.Tag ?? document.ETag;
        }

        async Task IHttpClientWrapper.PostJsonDocument<T>(string url, PostJsonDocumentOptions options, BaseDocument<T> document)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();
            await this.SetAuthorizationHeader();
            this.SetIfMatchHeader(document.ETag);

            // Perform request
            HttpResponseMessage response = await this._httpClient.PostAsJsonAsync(url, document.Content);
            this.EnsureResponseIsValid(response);

            document.LastModified = response.Content?.Headers.LastModified ?? document.LastModified;
            document.ETag = response.Headers.ETag?.Tag ?? document.ETag;
        }

        async Task IHttpClientWrapper.Delete(string url, DeleteOptions options)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();
            await this.SetAuthorizationHeader();
            if (!string.IsNullOrEmpty(options.ETag))
            {
                this.SetIfMatchHeader(options.ETag);
            }

            // Perform request
            HttpResponseMessage response = await this._httpClient.DeleteAsync(url);
            this.EnsureResponseIsValid(response);
        }

        #endregion

        #region Public methods

        public void SetConfiguration(EndpointConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (configuration.EndpointUri == null || !configuration.EndpointUri.IsAbsoluteUri)
            {
                throw new ArgumentException("The endpoint must be a valid absolute URI");
            }

            if (configuration.Version == null || !configuration.Version.IsSupported())
            {
                string supportedVersions = string.Join(", ", XApiVersion.SUPPORTED_VERSIONS);
                throw new ArgumentException($"Version is not supported. Supported versions are: {supportedVersions}");
            }

            this._httpClient = configuration.HttpClient;
            this._httpClient.BaseAddress = configuration.EndpointUri;
            this._httpClient.DefaultRequestHeaders.Add("X-Experience-API-Version", configuration.Version.ToString());

            this._authenticator = configuration.GetAuthenticator();
        }

        public void EnsureConfigured()
        {
            if (this._httpClient == null || this._authenticator == null)
            {
                throw new ConfigurationException($"xAPI client is not configured. Please call the {nameof(SetConfiguration)} method before accessing resources.");
            }
        }

        #endregion

        #region Utils

        private void ClearSpecificRequestHeaders()
        {
            this._httpClient.DefaultRequestHeaders.Authorization = null;
            this._httpClient.DefaultRequestHeaders.Accept.Clear();
            this._httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
            this._httpClient.DefaultRequestHeaders.IfMatch.Clear();
            this._httpClient.DefaultRequestHeaders.IfNoneMatch.Clear();
        }

        private async Task SetAuthorizationHeader()
        {
            AuthorizationHeaderInfos authorization = await this._authenticator.GetAuthorization();
            if (authorization != null)
            {
                this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    authorization.Scheme, authorization.Parameter
                );
            }
            else
            {
                this._httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        private void SetAcceptJsonContentType()
        {
            this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void SetAcceptedLanguages(List<string> acceptedLanguages)
        {
            if (acceptedLanguages != null)
            {
                foreach (string language in acceptedLanguages)
                {
                    this._httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
                }
            }
        }

        private void SetIfMatchHeader(string etag)
        {
            if (!string.IsNullOrEmpty(etag))
            {
                this._httpClient.DefaultRequestHeaders.IfMatch.Add(new EntityTagHeaderValue(etag));
            }
            else
            {
                this._httpClient.DefaultRequestHeaders.IfNoneMatch.Add(EntityTagHeaderValue.Any);
            }
        }

        private void EnsureResponseIsValid(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException(response.ReasonPhrase);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException(response.ReasonPhrase);
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new ForbiddenException(response.ReasonPhrase);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException(response.ReasonPhrase);
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ConflictException(response.ReasonPhrase);
            }
            else if (response.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                throw new PreConditionFailedException(response.ReasonPhrase);
            }
            else if (response.StatusCode == HttpStatusCode.RequestEntityTooLarge)
            {
                throw new EntityTooLargeException(response.ReasonPhrase);
            }
            else if (response.StatusCode == (HttpStatusCode)429)
            {
                throw new TooManyRequestsException(response.ReasonPhrase);
            }
            else
            {
                response.EnsureSuccessStatusCode();
            }
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this._httpClient.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        #endregion
    }

    internal class GetJsonOptions
    {
        public List<string> AcceptedLanguages { get; set; }
    }

    internal class PutJsonOptions
    {
    }

    internal class PostJsonOptions
    {
    }

    internal class GetJsonDocumentOptions
    {
    }

    internal class PutJsonDocumentOptions
    {
    }

    internal class PostJsonDocumentOptions
    {
    }

    internal class DeleteOptions
    {
        public string ETag { get; set; }
    }
}
