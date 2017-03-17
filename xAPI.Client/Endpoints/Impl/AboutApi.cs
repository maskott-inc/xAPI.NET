using System.Threading.Tasks;
using xAPI.Client.Http;
using xAPI.Client.Http.Options;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints.Impl
{
    internal class AboutApi : IAboutApi
    {
        private const string ENDPOINT = "about";
        private readonly IHttpClientWrapper _client;

        public AboutApi(IHttpClientWrapper client)
        {
            this._client = client;
        }

        #region IAboutApi members

        async Task<About> IAboutApi.Get()
        {
            var options = new RequestOptions(ENDPOINT);
            HttpResult<About> result = await this._client.GetJson<About>(options);
            return result.Content;
        }

        #endregion
    }
}
