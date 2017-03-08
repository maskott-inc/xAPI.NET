using System;
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
        Task<T> GetJson<T>(string url);
        Task GetDocumentAsJson<T>(string url, BaseDocument<T> document);
        Task PutDocumentAsJson<T>(string url, BaseDocument<T> document);
        Task PostDocumentAsJson<T>(string url, BaseDocument<T> document);
        Task Delete(string url, string etag);
    }

    internal class HttpClientWrapper : IHttpClientWrapper, IDisposable
    {
        private HttpClient _httpClient { get; set; }
        private ILRSAuthenticator _authenticator { get; set; }

        #region IHttpClientWrapper members

        async Task<T> IHttpClientWrapper.GetJson<T>(string url)
        {
            return await this.GetJson<T>(url);
        }

        async Task IHttpClientWrapper.GetDocumentAsJson<T>(string url, BaseDocument<T> document)
        {
            await this.GetDocumentAsJson<T>(url, document);
        }

        async Task IHttpClientWrapper.PutDocumentAsJson<T>(string url, BaseDocument<T> document)
        {
            await this.PutDocumentAsJson<T>(url, document);
        }

        async Task IHttpClientWrapper.PostDocumentAsJson<T>(string url, BaseDocument<T> document)
        {
            await this.PostDocumentAsJson<T>(url, document);
        }

        async Task IHttpClientWrapper.Delete(string url, string etag)
        {
            await this.Delete(url, etag);
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

        private async Task<T> GetJson<T>(string url)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();
            await this.SetAuthorizationHeader();
            this.SetAcceptJsonContentType();

            // Perform request
            HttpResponseMessage response = await this._httpClient.GetAsync(url);
            this.EnsureResponseIsValid(response);

            // Parse content
            return await response.Content.ReadAsAsync<T>();
        }

        private async Task GetDocumentAsJson<T>(string url, BaseDocument<T> document)
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

        private async Task PutDocumentAsJson<T>(string url, BaseDocument<T> document)
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

        private async Task PostDocumentAsJson<T>(string url, BaseDocument<T> document)
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

        private async Task Delete(string url, string etag)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();
            await this.SetAuthorizationHeader();
            if (!string.IsNullOrEmpty(etag))
            {
                this.SetIfMatchHeader(etag);
            }

            // Perform request
            HttpResponseMessage response = await this._httpClient.DeleteAsync(url);
            this.EnsureResponseIsValid(response);
        }

        private void ClearSpecificRequestHeaders()
        {
            this._httpClient.DefaultRequestHeaders.Authorization = null;
            this._httpClient.DefaultRequestHeaders.Accept.Clear();
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
}
