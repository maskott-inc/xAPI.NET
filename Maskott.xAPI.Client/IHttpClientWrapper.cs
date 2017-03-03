using Maskott.xAPI.Client.Authenticators;
using Maskott.xAPI.Client.Exceptions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client
{
    internal interface IHttpClientWrapper
    {
        Task<T> Get<T>(string url);
    }

    internal class HttpClientWrapper : IHttpClientWrapper
    {
        internal HttpClient HttpClient { get; set; }
        internal ILRSAuthenticator Authenticator { get; set; }

        #region IHttpClientWrapper members

        async Task<T> IHttpClientWrapper.Get<T>(string url)
        {
            return await this.Get<T>(url);
        }

        #endregion

        #region Utils

        private async Task<T> Get<T>(string url)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();

            // Ensure authorization is valid
            await this.SetAuthorizationHeader();

            // Perform request
            HttpResponseMessage response = await this.HttpClient.GetAsync(url);

            // Handle response
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                // Credentials are valid but user is not allowed to
                // perform this operation on this resource
                throw new ForbiddenException("Invalid xAPI credentials");
            }
            else
            {
                response.EnsureSuccessStatusCode();
            }

            // Parse content
            return await response.Content.ReadAsAsync<T>();
        }

        private void ClearSpecificRequestHeaders()
        {
            this.HttpClient.DefaultRequestHeaders.Authorization = null;
            this.HttpClient.DefaultRequestHeaders.IfMatch.Clear();
            this.HttpClient.DefaultRequestHeaders.IfNoneMatch.Clear();
        }

        private async Task SetAuthorizationHeader()
        {
            AuthorizationHeaderInfos authorization = await this.Authenticator.GetAuthorization();
            if (authorization != null)
            {
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    authorization.Scheme, authorization.Parameter
                );
            }
            else
            {
                this.HttpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        private void SetIfMatchHeader(string etag)
        {
            if (!string.IsNullOrEmpty(etag))
            {
                this.HttpClient.DefaultRequestHeaders.IfMatch.Add(new EntityTagHeaderValue(etag));
            }
        }

        private void SetIfNoneMatchHeader(string etag)
        {
            if (string.IsNullOrEmpty(etag))
            {
                this.HttpClient.DefaultRequestHeaders.IfNoneMatch.Add(new EntityTagHeaderValue("*"));
            }
            else
            {
                this.HttpClient.DefaultRequestHeaders.IfNoneMatch.Add(new EntityTagHeaderValue(etag));
            }
        }

        #endregion
    }
}
