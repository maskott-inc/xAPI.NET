using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints.Impl
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
            return this.Get<StateDocument>(request);
        }

        Task<StateDocument<T>> IStatesApi.Get<T>(GetStateRequest request)
        {
            return this.Get<StateDocument<T>>(request);
        }

        Task IStatesApi.Put<T>(PutStateRequest request, StateDocument<T> state)
        {
            throw new NotImplementedException();
        }

        Task IStatesApi.Post<T>(PostStateRequest request, StateDocument<T> state)
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

        #region Utils

        private Task<T> Get<T>(GetStateRequest request)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
