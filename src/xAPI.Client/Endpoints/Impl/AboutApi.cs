using System.Net.Http;
using System.Threading.Tasks;
using xAPI.Client.Http;
using xAPI.Client.Http.Options;
using xAPI.Client.Json;
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

        async Task<About> IAboutApi.Get()
        {
            var options = new RequestOptions(ENDPOINT);
            HttpResponseMessage response = await this._client.GetJson(options);
            return await response.Content.ReadAsAsync<About>(new[] { new StrictJsonMediaTypeFormatter() });
        }
    }
}
