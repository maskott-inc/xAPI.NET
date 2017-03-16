using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using xAPI.Client.Authenticators;
using xAPI.Client.Configuration;
using xAPI.Client.Exceptions;
using xAPI.Client.Json;
using xAPI.Client.Resources;

namespace xAPI.Client.Http
{
    internal class HttpClientWrapper : IHttpClientWrapper, IDisposable
    {
        private const string XAPI_VERSION_HEADER = "X-Experience-API-Version";
        private HttpClient _httpClient;
        private ILRSAuthenticator _authenticator;

        #region IHttpClientWrapper members

        async Task<T> IHttpClientWrapper.GetJson<T>(string url, GetJsonOptions options, Action<HttpResponseMessage, T> onResponse)
        {
            StrictJsonMediaTypeFormatter formatter = this.GetFormatter(options);

            // Initialize request
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            await this.SetAuthorizationHeader(request);
            this.SetAcceptJsonContentType(request);
            this.SetAcceptedLanguages(request, options.AcceptedLanguages);

            // Perform request
            HttpResponseMessage response = await this._httpClient.SendAsync(request);
            this.EnsureResponseIsValid(response);

            // Parse content
            T result = await response.Content.ReadAsAsync<T>(new[] { formatter });

            // Custom reponse callback
            onResponse?.Invoke(response, result);

            return result;
        }

        async Task IHttpClientWrapper.PutJson<T>(string url, PutJsonOptions options, T content)
        {
            StrictJsonMediaTypeFormatter formatter = this.GetFormatter(options);

            // Initialize request
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            await this.SetAuthorizationHeader(request);
            request.Content = new ObjectContent<T>(content, formatter);

            // Perform request
            HttpResponseMessage response = await this._httpClient.SendAsync(request);
            this.EnsureResponseIsValid(response);
        }

        async Task IHttpClientWrapper.PostJson<T>(string url, PostJsonOptions options, T content)
        {
            StrictJsonMediaTypeFormatter formatter = this.GetFormatter(options);

            // Initialize request
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            await this.SetAuthorizationHeader(request);
            request.Content = new ObjectContent<T>(content, formatter);

            // Perform request
            HttpResponseMessage response = await this._httpClient.SendAsync(request);
            this.EnsureResponseIsValid(response);
        }

        async Task IHttpClientWrapper.GetJsonDocument<T>(string url, GetJsonDocumentOptions options, BaseDocument<T> document)
        {
            StrictJsonMediaTypeFormatter formatter = this.GetFormatter(options);

            // Initialize request
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            await this.SetAuthorizationHeader(request);
            this.SetAcceptJsonContentType(request);

            // Perform request
            HttpResponseMessage response = await this._httpClient.SendAsync(request);
            this.EnsureResponseIsValid(response);

            // Parse content
            document.LastModified = response.Content.Headers.LastModified;
            document.ETag = response.Headers.ETag?.Tag;
            document.Content = await response.Content.ReadAsAsync<T>(new[] { formatter });
        }

        async Task IHttpClientWrapper.PutJsonDocument<T>(string url, PutJsonDocumentOptions options, BaseDocument<T> document)
        {
            StrictJsonMediaTypeFormatter formatter = this.GetFormatter(options);

            // Initialize request
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            await this.SetAuthorizationHeader(request);
            this.SetIfMatchHeader(request, document.ETag);
            request.Content = new ObjectContent<T>(document.Content, formatter);

            // Perform request
            HttpResponseMessage response = await this._httpClient.SendAsync(request);
            this.EnsureResponseIsValid(response);

            document.LastModified = response.Content?.Headers.LastModified ?? document.LastModified;
            document.ETag = response.Headers.ETag?.Tag ?? document.ETag;
        }

        async Task IHttpClientWrapper.PostJsonDocument<T>(string url, PostJsonDocumentOptions options, BaseDocument<T> document)
        {
            StrictJsonMediaTypeFormatter formatter = this.GetFormatter(options);

            // Initialize request
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            await this.SetAuthorizationHeader(request);
            this.SetIfMatchHeader(request, document.ETag);
            request.Content = new ObjectContent<T>(document.Content, formatter);

            // Perform request
            HttpResponseMessage response = await this._httpClient.SendAsync(request);
            this.EnsureResponseIsValid(response);

            document.LastModified = response.Content?.Headers.LastModified ?? document.LastModified;
            document.ETag = response.Headers.ETag?.Tag ?? document.ETag;
        }

        async Task IHttpClientWrapper.Delete(string url, DeleteOptions options)
        {
            // Handle specific headers
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            await this.SetAuthorizationHeader(request);
            if (!string.IsNullOrEmpty(options.ETag))
            {
                this.SetIfMatchHeader(request, options.ETag);
            }

            // Perform request
            HttpResponseMessage response = await this._httpClient.SendAsync(request);
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
            this._httpClient.DefaultRequestHeaders.Add(XAPI_VERSION_HEADER, configuration.Version.ToString());

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

        private StrictJsonMediaTypeFormatter GetFormatter(BaseJsonOptions options)
        {
            var formatter = new StrictJsonMediaTypeFormatter();
            formatter.SerializerSettings.NullValueHandling = options.NullValueHandling;
            return formatter;
        }

        private async Task SetAuthorizationHeader(HttpRequestMessage request)
        {
            AuthorizationHeaderInfos authorization = await this._authenticator.GetAuthorization();
            if (authorization != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(
                    authorization.Scheme, authorization.Parameter
                );
            }
            else
            {
                request.Headers.Authorization = null;
            }
        }

        private void SetAcceptJsonContentType(HttpRequestMessage request)
        {
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void SetAcceptedLanguages(HttpRequestMessage request, List<string> acceptedLanguages)
        {
            if (acceptedLanguages != null)
            {
                foreach (string language in acceptedLanguages)
                {
                    request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
                }
            }
        }

        private void SetIfMatchHeader(HttpRequestMessage request, string etag)
        {
            if (!string.IsNullOrEmpty(etag))
            {
                request.Headers.IfMatch.Add(new EntityTagHeaderValue(etag));
            }
            else
            {
                request.Headers.IfNoneMatch.Add(EntityTagHeaderValue.Any);
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
