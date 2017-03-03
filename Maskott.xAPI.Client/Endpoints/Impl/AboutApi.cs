using Maskott.xAPI.Client.Resources;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Endpoints.Impl
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

        Task<About> IAboutApi.Get()
        {
            return this.Get<About>();
        }

        Task<About<T>> IAboutApi.Get<T>()
        {
            return this.Get<About<T>>();
        }

        #endregion

        #region Utils

        private Task<T> Get<T>()
        {
            string url = ENDPOINT;
            return this._client.Get<T>(url);
        }

        #endregion
    }
}
