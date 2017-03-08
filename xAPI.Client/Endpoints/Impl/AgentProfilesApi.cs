﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xAPI.Client.Exceptions;
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
            string url = this.BuildUrl(request);
            var document = new AgentProfileDocument();
            await this._client.GetDocumentAsJson(url, document);
            return document;
        }

        async Task<AgentProfileDocument<T>> IAgentProfilesApi.Get<T>(GetAgentProfileRequest request)
        {
            string url = this.BuildUrl(request);
            var document = Activator.CreateInstance<AgentProfileDocument<T>>();
            await this._client.GetDocumentAsJson(url, document);
            return document;
        }

        async Task<bool> IAgentProfilesApi.Put<T>(PutAgentProfileRequest<T> request)
        {
            string url = this.BuildUrl(request);

            try
            {
                await this._client.PutDocumentAsJson(url, request.AgentProfile);
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<bool> IAgentProfilesApi.Post<T>(PostAgentProfileRequest<T> request)
        {
            string url = this.BuildUrl(request);

            try
            {
                await this._client.PostDocumentAsJson(url, request.AgentProfile);
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<bool> IAgentProfilesApi.Delete(DeleteAgentProfileRequest request)
        {
            string url = this.BuildUrl(request);

            try
            {
                await this._client.Delete(url, request.ETag);
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<List<string>> IAgentProfilesApi.GetMany(GetAgentProfilesRequest request)
        {
            string url = this.BuildUrl(request);

            return await this._client.GetJson<List<string>>(url);
        }

        #endregion

        #region Utils

        private string BuildUrl(ASingleAgentProfileRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var builder = new StringBuilder(ENDPOINT);
            string agentStr = JsonConvert.SerializeObject(request.Agent);
            builder.AppendFormat("?agent={0}", Uri.EscapeDataString(agentStr));
            builder.AppendFormat("&profileId={0}", Uri.EscapeDataString(request.ProfileId));

            return builder.ToString();
        }

        private string BuildUrl(GetAgentProfilesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var builder = new StringBuilder(ENDPOINT);
            string agentStr = JsonConvert.SerializeObject(request.Agent);
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
