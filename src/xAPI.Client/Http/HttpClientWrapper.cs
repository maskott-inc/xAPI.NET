using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using xAPI.Client.Authenticators;
using xAPI.Client.Configuration;
using xAPI.Client.Exceptions;
using xAPI.Client.Http.Options;
using xAPI.Client.Resources;

namespace xAPI.Client.Http
{
    internal class HttpClientWrapper : IHttpClientWrapper, IDisposable
    {
        private const string XAPI_VERSION_HEADER = "X-Experience-API-Version";
        private HttpClient _httpClient;
        private bool _useOwnHttpClient = false;
        private Uri _baseUri;
        private XApiVersion _version;
        private ILRSAuthenticator _authenticator;

        #region IHttpClientWrapper members

        async Task<HttpResponseMessage> IHttpClientWrapper.GetJson(RequestOptions options)
        {
            // Initialize request
            Uri url = new Uri(this._baseUri, options.PathAndQuery);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            this.SetXApiVersionHeader(request);
            await this.SetAuthorizationHeader(request);
            this.SetAcceptJsonContentType(request);
            this.SetCustomHeaders(request, options);

            // Perform request
            HttpResponseMessage response = await this._httpClient.SendAsync(request);
            this.EnsureResponseIsValid(response);

            return response;
        }

        async Task<HttpResponseMessage> IHttpClientWrapper.PutJson<T>(RequestOptions options, T content)
        {
            // Initialize request
            Uri url = new Uri(this._baseUri, options.PathAndQuery);
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            this.SetXApiVersionHeader(request);
            await this.SetAuthorizationHeader(request);
            this.SetCustomHeaders(request, options);
            request.Content = new ObjectContent<T>(content, options.GetFormatter());

            // Perform request
            HttpResponseMessage response = await this._httpClient.SendAsync(request);
            this.EnsureResponseIsValid(response);

            return response;
        }

        async Task<HttpResponseMessage> IHttpClientWrapper.PostJson<T>(RequestOptions options, T content)
        {
            // Initialize request
            Uri url = new Uri(this._baseUri, options.PathAndQuery);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            this.SetXApiVersionHeader(request);
            await this.SetAuthorizationHeader(request);
            this.SetCustomHeaders(request, options);
            request.Content = new ObjectContent<T>(content, options.GetFormatter());

            // Perform request
            HttpResponseMessage response = await this._httpClient.SendAsync(request);
            this.EnsureResponseIsValid(response);

            return response;
        }

        async Task<HttpResponseMessage> IHttpClientWrapper.Delete(RequestOptions options)
        {
            // Handle specific headers
            Uri url = new Uri(this._baseUri, options.PathAndQuery);
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            this.SetXApiVersionHeader(request);
            await this.SetAuthorizationHeader(request);
            this.SetCustomHeaders(request, options);

            // Perform request
            HttpResponseMessage response = await this._httpClient.SendAsync(request);
            this.EnsureResponseIsValid(response);

            return response;
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

            this._useOwnHttpClient = (configuration.HttpClient == null);
            this._httpClient = configuration.HttpClient ?? new HttpClient();
            this._baseUri = configuration.EndpointUri;
            this._version = configuration.Version;

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

        private void SetXApiVersionHeader(HttpRequestMessage request)
        {
            request.Headers.Add(XAPI_VERSION_HEADER, this._version.ToString());
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

        private void SetCustomHeaders(HttpRequestMessage request, RequestOptions options)
        {
            foreach (KeyValuePair<string, string> header in options.CustomHeaders)
            {
                request.Headers.Add(header.Key, header.Value);
            }
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

        private void EnsureResponseIsValid(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            // The content won't be used, so we don't need it anymore
            response.Content?.Dispose();

            // Throws appropriate HTTP exception
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
                throw new UnexpectedHttpException(response.StatusCode, response.ReasonPhrase);
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
                    if (this._useOwnHttpClient)
                    {
                        this._httpClient?.Dispose();
                        this._httpClient = null;
                    }
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
