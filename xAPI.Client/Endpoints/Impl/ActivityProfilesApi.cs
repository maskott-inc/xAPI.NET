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
    internal class ActivityProfilesApi : IActivityProfilesApi
    {
        private const string ENDPOINT = "activities/profile";
        private readonly IHttpClientWrapper _client;

        public ActivityProfilesApi(IHttpClientWrapper client)
        {
            this._client = client;
        }

        #region IActivityProfilesApi members

        async Task<ActivityProfileDocument> IActivityProfilesApi.Get(GetActivityProfileRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            HttpResult<JToken> result = await this._client.GetJson<JToken>(options);

            var document = new ActivityProfileDocument();
            document.ETag = result.Headers.ETag?.Tag;
            document.LastModified = result.ContentHeaders.LastModified;
            document.Content = result.Content;

            return document;
        }

        async Task<ActivityProfileDocument<T>> IActivityProfilesApi.Get<T>(GetActivityProfileRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            HttpResult<T> result = await this._client.GetJson<T>(options);

            var document = new ActivityProfileDocument<T>();
            document.ETag = result.Headers.ETag?.Tag;
            document.LastModified = result.ContentHeaders.LastModified;
            document.Content = result.Content;

            return document;
        }

        async Task<bool> IActivityProfilesApi.Put<T>(PutActivityProfileRequest<T> request)
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
                HttpResult result = await this._client.PutJson(options, request.ActivityProfile);
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<bool> IActivityProfilesApi.Post<T>(PostActivityProfileRequest<T> request)
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
                HttpResult result = await this._client.PostJson(options, request.ActivityProfile);
                return true;
            }
            catch (PreConditionFailedException)
            {
                return false;
            }
        }

        async Task<bool> IActivityProfilesApi.Delete(DeleteActivityProfileRequest request)
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

        async Task<List<string>> IActivityProfilesApi.GetMany(GetActivityProfilesRequest request)
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

        private void CompleteOptionsBase(RequestOptions options, ASingleActivityProfileRequest request)
        {
            options.QueryStringParameters.Add("activityId", request.ActivityId.ToString());
            options.QueryStringParameters.Add("profileId", request.ProfileId);
        }

        private void CompleteOptions(RequestOptions options, GetActivityProfileRequest request)
        {
            this.CompleteOptionsBase(options, request);
        }

        private void CompleteOptions<T>(RequestOptions options, PostActivityProfileRequest<T> request)
        {
            this.CompleteOptionsBase(options, request);
            this.AddETagHeader(options, request.ActivityProfile.ETag);
        }

        private void CompleteOptions<T>(RequestOptions options, PutActivityProfileRequest<T> request)
        {
            this.CompleteOptionsBase(options, request);
            this.AddETagHeader(options, request.ActivityProfile.ETag);
        }

        private void CompleteOptions(RequestOptions options, DeleteActivityProfileRequest request)
        {
            this.CompleteOptionsBase(options, request);
            this.AddETagHeader(options, request.ETag);
        }

        private void CompleteOptions(RequestOptions options, GetActivityProfilesRequest request)
        {
            options.QueryStringParameters.Add("activityId", request.ActivityId.ToString());
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
