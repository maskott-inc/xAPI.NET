using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xAPI.Client.Exceptions;
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
            string url = this.BuildUrl(request);
            var document = new StateDocument();
            await this._client.GetJsonDocument(url, new GetJsonDocumentOptions(), document);
            return document;
        }

        async Task<StateDocument<T>> IStatesApi.Get<T>(GetStateRequest request)
        {
            string url = this.BuildUrl(request);
            var document = Activator.CreateInstance<StateDocument<T>>();
            await this._client.GetJsonDocument(url, new GetJsonDocumentOptions(), document);
            return document;
        }

        async Task<bool> IStatesApi.Put<T>(PutStateRequest<T> request)
        {
            string url = this.BuildUrl(request);

            try
            {
                await this._client.PutJsonDocument(url, new PutJsonDocumentOptions(), request.State);
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<bool> IStatesApi.Post<T>(PostStateRequest<T> request)
        {
            string url = this.BuildUrl(request);

            try
            {
                await this._client.PostJsonDocument(url, new PostJsonDocumentOptions(), request.State);
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<bool> IStatesApi.Delete(DeleteStateRequest request)
        {
            string url = this.BuildUrl(request);

            try
            {
                await this._client.Delete(url, new DeleteOptions() { ETag = request.ETag });
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<List<string>> IStatesApi.GetMany(GetStatesRequest request)
        {
            string url = this.BuildUrl(request);

            return await this._client.GetJson<List<string>>(url, new GetJsonOptions());
        }

        async Task IStatesApi.DeleteMany(DeleteStatesRequest request)
        {
            string url = this.BuildUrl(request);

            await this._client.Delete(url, new DeleteOptions());
        }

        #endregion

        #region Utils

        private string BuildUrl(ASingleStateRequest request)
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

            return builder.ToString();
        }

        private string BuildUrl(GetStatesRequest request)
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
            if (request.Since.HasValue)
            {
                builder.AppendFormat("&since={0}", Uri.EscapeDataString(request.Since.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")));
            }

            return builder.ToString();
        }

        private string BuildUrl(DeleteStatesRequest request)
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

            return builder.ToString();
        }

        #endregion
    }
}
