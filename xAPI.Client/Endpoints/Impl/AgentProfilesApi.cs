using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xAPI.Client.Exceptions;
using xAPI.Client.Http;
using xAPI.Client.Http.Options;
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

        async Task<AgentProfileDocument> IAgentProfilesApi.Get(GetAgentProfileRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            string url = this.BuildUrl(request);
            var document = new AgentProfileDocument();
            await this._client.GetJsonDocument(url, new GetJsonDocumentOptions(), document);
            return document;
        }

        async Task<AgentProfileDocument<T>> IAgentProfilesApi.Get<T>(GetAgentProfileRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            string url = this.BuildUrl(request);
            var document = Activator.CreateInstance<AgentProfileDocument<T>>();
            await this._client.GetJsonDocument(url, new GetJsonDocumentOptions(), document);
            return document;
        }

        async Task<bool> IAgentProfilesApi.Put<T>(PutAgentProfileRequest<T> request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            string url = this.BuildUrl(request);

            try
            {
                await this._client.PutJsonDocument(url, new PutJsonDocumentOptions(), request.AgentProfile);
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<bool> IAgentProfilesApi.Post<T>(PostAgentProfileRequest<T> request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            string url = this.BuildUrl(request);

            try
            {
                await this._client.PostJsonDocument(url, new PostJsonDocumentOptions(), request.AgentProfile);
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<bool> IAgentProfilesApi.Delete(DeleteAgentProfileRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

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

        async Task<List<string>> IAgentProfilesApi.GetMany(GetAgentProfilesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            string url = this.BuildUrl(request);

            return await this._client.GetJson<List<string>>(url, new GetJsonOptions());
        }

        #endregion

        #region Utils

        private string BuildUrl(ASingleAgentProfileRequest request)
        {
            var builder = new StringBuilder(ENDPOINT);
            string agentStr = JsonConvert.SerializeObject(request.Agent, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            builder.AppendFormat("?agent={0}", Uri.EscapeDataString(agentStr));
            builder.AppendFormat("&profileId={0}", Uri.EscapeDataString(request.ProfileId));

            return builder.ToString();
        }

        private string BuildUrl(GetAgentProfilesRequest request)
        {
            var builder = new StringBuilder(ENDPOINT);
            string agentStr = JsonConvert.SerializeObject(request.Agent, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            builder.AppendFormat("?agent={0}", Uri.EscapeDataString(agentStr));
            if (request.Since.HasValue)
            {
                builder.AppendFormat("&since={0}", Uri.EscapeDataString(request.Since.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")));
            }

            return builder.ToString();
        }

        #endregion
    }
}
