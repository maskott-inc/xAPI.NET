using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            HttpResult<JToken> result = await this._client.GetJson<JToken>(options);

            var document = new AgentProfileDocument();
            document.ETag = result.Headers.ETag?.Tag;
            document.LastModified = result.ContentHeaders.LastModified;
            document.Content = result.Content;

            return document;
        }

        async Task<AgentProfileDocument<T>> IAgentProfilesApi.Get<T>(GetAgentProfileRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            HttpResult<T> result = await this._client.GetJson<T>(options);

            var document = new AgentProfileDocument<T>();
            document.ETag = result.Headers.ETag?.Tag;
            document.LastModified = result.ContentHeaders.LastModified;
            document.Content = result.Content;

            return document;
        }

        async Task<bool> IAgentProfilesApi.Put<T>(PutAgentProfileRequest<T> request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            try
            {
                HttpResult result = await this._client.PutJson(options, request.AgentProfile);
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

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            try
            {
                HttpResult result = await this._client.PostJson(options, request.AgentProfile);
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

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            try
            {
                HttpResult result = await this._client.Delete(options);
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

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            HttpResult<List<string>> result = await this._client.GetJson<List<string>>(options);
            return result.Content;
        }

        #endregion

        #region Utils

        private void CompleteOptionsBase(RequestOptions options, ASingleAgentProfileRequest request)
        {
            string agentStr = JsonConvert.SerializeObject(request.Agent, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            options.QueryStringParameters.Add("agent", agentStr);
            options.QueryStringParameters.Add("profileId", request.ProfileId);
        }

        private void CompleteOptions(RequestOptions options, GetAgentProfileRequest request)
        {
            this.CompleteOptionsBase(options, request);
        }

        private void CompleteOptions<T>(RequestOptions options, PostAgentProfileRequest<T> request)
        {
            this.CompleteOptionsBase(options, request);
            this.AddETagHeader(options, request.AgentProfile.ETag);
        }

        private void CompleteOptions<T>(RequestOptions options, PutAgentProfileRequest<T> request)
        {
            this.CompleteOptionsBase(options, request);
            this.AddETagHeader(options, request.AgentProfile.ETag);
        }

        private void CompleteOptions(RequestOptions options, DeleteAgentProfileRequest request)
        {
            this.CompleteOptionsBase(options, request);
            this.AddETagHeader(options, request.ETag);
        }

        private void CompleteOptions(RequestOptions options, GetAgentProfilesRequest request)
        {
            string agentStr = JsonConvert.SerializeObject(request.Agent, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            options.QueryStringParameters.Add("agent", agentStr);
            if (request.Since.HasValue)
            {
                options.QueryStringParameters.Add("since", request.Since.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            }
        }

        private void AddETagHeader(RequestOptions options, string etag)
        {
            if (!string.IsNullOrEmpty(etag))
            {
                options.CustomHeaders.Add("If-Match", etag);
            }
            else
            {
                options.CustomHeaders.Add("If-None-Match", "*");
            }
        }

        #endregion
    }
}
