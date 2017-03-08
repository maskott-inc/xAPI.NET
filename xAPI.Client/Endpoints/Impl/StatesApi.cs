using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
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

        async Task<StateDocument> IStatesApi.Get(GetStateRequest request)
        {
            var document = new StateDocument();
            await this.Get(request, document);
            return document;
        }

        async Task<StateDocument<T>> IStatesApi.Get<T>(GetStateRequest request)
        {
            var document = Activator.CreateInstance<StateDocument<T>>();
            await this.Get(request, document);
            return document;
        }

        Task IStatesApi.Put<T>(PutStateRequest<T> request)
        {
            throw new NotImplementedException();
        }

        Task IStatesApi.Post<T>(PostStateRequest<T> request)
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

        private async Task Get<T>(GetStateRequest request, StateDocument<T> document)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var builder = new StringBuilder(ENDPOINT);
            builder.AppendFormat("?activityId={0}", Uri.EscapeDataString(request.ActivityId.ToString()));
            string agentStr = JsonConvert.SerializeObject(request.Agent);
            builder.AppendFormat("&agent={0}", Uri.EscapeDataString(agentStr));
            if (request.Registration.HasValue)
            {
                builder.AppendFormat("&registration={0}", Uri.EscapeDataString(request.Registration.Value.ToString()));
            }
            builder.AppendFormat("&stateId={0}", Uri.EscapeDataString(request.StateId));

            await this._client.GetDocumentAsJson(document, builder.ToString(), throwIfNotFound: true);
        }

        #endregion
    }
}
