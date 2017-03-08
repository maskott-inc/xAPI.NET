using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints.Impl
{
    internal class AgentProfilesApi : IAgentProfilesApi
    {
        private const string ENDPOINT = "agents/profile";
        private readonly IHttpClientWrapper _client;

        public AgentProfilesApi(IHttpClientWrapper client)
        {
            this._client = client;
        }

        #region IAgentProfilesApi members

        Task<AgentProfileDocument> IAgentProfilesApi.Get(GetAgentProfileRequest request)
        {
            throw new NotImplementedException();
        }

        Task IAgentProfilesApi.Put<T>(PutAgentProfileRequest<T> request)
        {
            throw new NotImplementedException();
        }

        Task IAgentProfilesApi.Post<T>(PostAgentProfileRequest<T> request)
        {
            throw new NotImplementedException();
        }

        Task IAgentProfilesApi.Delete(DeleteAgentProfileRequest request)
        {
            throw new NotImplementedException();
        }

        Task<List<string>> IAgentProfilesApi.GetMany(GetAgentProfilesRequest request)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
