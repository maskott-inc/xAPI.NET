using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using xAPI.Client.Http;
using xAPI.Client.Http.Options;
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

        async Task<Person> IAgentsApi.Get(GetAgentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            string agentStr = JsonConvert.SerializeObject(request.Agent, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            string url = string.Format("{0}?agent={1}", ENDPOINT, Uri.EscapeDataString(agentStr));
            return await this._client.GetJson<Person>(url, new GetJsonOptions());
        }

        #endregion
    }
}
