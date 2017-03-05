using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using xAPI.Client.Authenticators;
using xAPI.Client.Exceptions;

namespace xAPI.Client
{
    internal interface IHttpClientWrapper
    {
        Task<T> GetJson<T>(string url);
    }

    internal class HttpClientWrapper : IHttpClientWrapper
    {
        internal HttpClient HttpClient { get; set; }
        internal ILRSAuthenticator Authenticator { get; set; }

        #region IHttpClientWrapper members

        async Task<T> IHttpClientWrapper.GetJson<T>(string url)
        {
            return await this.GetJson<T>(url);
        }

        #endregion

        #region Utils

        private async Task<T> GetJson<T>(string url)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();
            await this.SetAuthorizationHeader();
            this.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
            this.HttpClient.DefaultRequestHeaders.Accept.Clear();
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
