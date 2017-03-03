using Maskott.xAPI.Client.Requests;
using Maskott.xAPI.Client.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Endpoints.Impl
{
    internal class StatesApi : IStatesApi
    {
        private const string ENDPOINT = "activities/state";
        private readonly IHttpClientWrapper _client;

        public StatesApi(IHttpClientWrapper client)
        {
            this._client = client;
        }

        #region IStatesApi members

        Task<StateDocument> IStatesApi.Get(GetStateRequest request)
        {
            throw new NotImplementedException();
        }

        Task IStatesApi.Put(PutStateRequest request, StateDocument state)
        {
            throw new NotImplementedException();
        }

        Task IStatesApi.Post(PostStateRequest request, StateDocument state)
        {
            throw new NotImplementedException();
        }

        Task IStatesApi.Delete(DeleteStateRequest request)
        {
            throw new NotImplementedException();
        }

        Task<List<string>> IStatesApi.GetMany(GetStatesRequest request)
        {
            throw new NotImplementedException();
        }

        Task IStatesApi.DeleteMany(DeleteStatesRequest request)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
