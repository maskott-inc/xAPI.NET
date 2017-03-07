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
        Task<T> GetJson<T>(string url, bool throwIfNotFound);
        Task GetDocumentAsJson<T>(BaseDocument<T> document, string url, bool throwIfNotFound);
    }

    internal class HttpClientWrapper : IHttpClientWrapper, IDisposable
    {
        private HttpClient _httpClient { get; set; }
        private ILRSAuthenticator _authenticator { get; set; }

        #region IHttpClientWrapper members

        async Task<T> IHttpClientWrapper.GetJson<T>(string url, bool throwIfNotFound)
        {
            return await this.GetJson<T>(url, throwIfNotFound);
        }

        async Task IHttpClientWrapper.GetDocumentAsJson<T>(BaseDocument<T> document, string url, bool throwIfNotFound)
        {
            await this.GetDocumentAsJson<T>(document, url, throwIfNotFound);
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

        private async Task<T> GetJson<T>(string url, bool throwIfNotFound)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();
            await this.SetAuthorizationHeader();
            this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Perform request
            HttpResponseMessage response = await this._httpClient.GetAsync(url);

            // Handle response
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                // Credentials are valid but user is not allowed to
                // perform this operation on this resource
                throw new ForbiddenException("Invalid xAPI credentials");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound && !throwIfNotFound)
            {
                return default(T);
            }
            else
            {
                response.EnsureSuccessStatusCode();
            }

            // Parse content
            return await response.Content.ReadAsAsync<T>();
        }

        private async Task GetDocumentAsJson<T>(BaseDocument<T> document, string url, bool throwIfNotFound)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();
            await this.SetAuthorizationHeader();
            this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Perform request
            HttpResponseMessage response = await this._httpClient.GetAsync(url);

            // Handle response
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                // Credentials are valid but user is not allowed to
                // perform this operation on this resource
                throw new ForbiddenException("Invalid xAPI credentials");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound && !throwIfNotFound)
            {
                document.Content = default(T);
                return;
            }
            else
            {
                response.EnsureSuccessStatusCode();
            }

            document.LastModified = response.Content.Headers.LastModified;
            document.ETag = response.Headers.ETag?.Tag;
            document.Content = await response.Content.ReadAsAsync<T>();
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

        private void SetIfMatchHeader(string etag)
        {
            if (!string.IsNullOrEmpty(etag))
            {
                this._httpClient.DefaultRequestHeaders.IfMatch.Add(new EntityTagHeaderValue(etag));
            }
        }

        private void SetIfNoneMatchHeader(string etag)
        {
            if (string.IsNullOrEmpty(etag))
            {
                this._httpClient.DefaultRequestHeaders.IfNoneMatch.Add(new EntityTagHeaderValue("*"));
            }
            else
            {
                this._httpClient.DefaultRequestHeaders.IfNoneMatch.Add(new EntityTagHeaderValue(etag));
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
