using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints.Impl
{
    internal class AgentsApi : IAgentsApi
    {
        private const string ENDPOINT = "agents";
        private readonly IHttpClientWrapper _client;

        public AgentsApi(IHttpClientWrapper client)
        {
            this._client = client;
        }

        #region IAgentsApi members

        Task<Person> IAgentsApi.Get(GetAgentRequest request)
        {
            return this.Get<Person>(request);
        }

        #endregion

        #region Utils

        private Task<T> Get<T>(GetAgentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Agent == null)
            {
                throw new ArgumentNullException(nameof(request.Agent));
            }

            string agentStr = JsonConvert.SerializeObject(request.Agent);
            string url = string.Format("{0}?agent={1}", ENDPOINT, Uri.EscapeDataString(agentStr));
            return this._client.Get<T>(url);
        }

        #endregion
    }
}
