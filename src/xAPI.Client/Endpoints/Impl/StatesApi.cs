using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using xAPI.Client.Exceptions;
using xAPI.Client.Http;
using xAPI.Client.Http.Options;
using xAPI.Client.Json;
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

        async Task<StateDocument> IStatesApi.Get(GetStateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            HttpResponseMessage response = await this._client.GetJson(options);
            JToken content = await response.Content.ReadAsAsync<JToken>(new[] { new StrictJsonMediaTypeFormatter() });

            var document = new StateDocument();
            document.ETag = response.Headers.ETag?.Tag;
            document.LastModified = response.Content.Headers.LastModified;
            document.Content = content;

            return document;
        }

        async Task<StateDocument<T>> IStatesApi.Get<T>(GetStateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            HttpResponseMessage response = await this._client.GetJson(options);
            T content = await response.Content.ReadAsAsync<T>(new[] { new StrictJsonMediaTypeFormatter() });

            var document = new StateDocument<T>();
            document.ETag = response.Headers.ETag?.Tag;
            document.LastModified = response.Content.Headers.LastModified;
            document.Content = content;

            return document;
        }

        async Task<bool> IStatesApi.Put<T>(PutStateRequest<T> request)
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
                await this._client.PutJson(options, request.State);
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<bool> IStatesApi.Post<T>(PostStateRequest<T> request)
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
                await this._client.PostJson(options, request.State);
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<bool> IStatesApi.Delete(DeleteStateRequest request)
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
                await this._client.Delete(options);
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<List<string>> IStatesApi.GetMany(GetStatesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            HttpResponseMessage response = await this._client.GetJson(options);
            return await response.Content.ReadAsAsync<List<string>>(new[] { new StrictJsonMediaTypeFormatter() });
        }

        async Task IStatesApi.DeleteMany(DeleteStatesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            await this._client.Delete(options);
        }

        private void CompleteOptionsBase(RequestOptions options, ASingleStateRequest request)
        {
            options.QueryStringParameters.Add("activityId", request.ActivityId.ToString());
            string agentStr = JsonConvert.SerializeObject(request.Agent, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            options.QueryStringParameters.Add("agent", agentStr);
            if (request.Registration.HasValue)
            {
                options.QueryStringParameters.Add("registration", request.Registration.Value.ToString());
            }
            options.QueryStringParameters.Add("stateId", request.StateId);
        }

        private void CompleteOptions(RequestOptions options, GetStateRequest request)
        {
            this.CompleteOptionsBase(options, request);
        }

        private void CompleteOptions<T>(RequestOptions options, PostStateRequest<T> request)
        {
            this.CompleteOptionsBase(options, request);
            this.AddETagHeader(options, request.State.ETag);
        }

        private void CompleteOptions<T>(RequestOptions options, PutStateRequest<T> request)
        {
            this.CompleteOptionsBase(options, request);
            this.AddETagHeader(options, request.State.ETag);
        }

        private void CompleteOptions(RequestOptions options, DeleteStateRequest request)
        {
            this.CompleteOptionsBase(options, request);
            if (!string.IsNullOrEmpty(request.ETag))
            {
                this.AddETagHeader(options, request.ETag);
            }
        }

        private void CompleteOptions(RequestOptions options, GetStatesRequest request)
        {
            options.QueryStringParameters.Add("activityId", request.ActivityId.ToString());
            string agentStr = JsonConvert.SerializeObject(request.Agent, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            options.QueryStringParameters.Add("agent", agentStr);
            if (request.Registration.HasValue)
            {
                options.QueryStringParameters.Add("registration", request.Registration.Value.ToString());
            }
            if (request.Since.HasValue)
            {
                options.QueryStringParameters.Add("since", request.Since.Value.ToString(Constants.DATETIME_FORMAT));
            }
        }

        private void CompleteOptions(RequestOptions options, DeleteStatesRequest request)
        {
            options.QueryStringParameters.Add("activityId", request.ActivityId.ToString());
            string agentStr = JsonConvert.SerializeObject(request.Agent, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            options.QueryStringParameters.Add("agent", agentStr);
            if (request.Registration.HasValue)
            {
                options.QueryStringParameters.Add("registration", request.Registration.Value.ToString());
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
    }
}
