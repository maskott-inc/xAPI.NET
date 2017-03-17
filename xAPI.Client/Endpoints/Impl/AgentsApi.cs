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

            var options = new RequestOptions(ENDPOINT);
            string agentStr = JsonConvert.SerializeObject(request.Agent, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            options.QueryStringParameters.Add("agent", agentStr);

            HttpResult<Person> result = await this._client.GetJson<Person>(options);
            return result.Content;
        }

        #endregion
    }
}
