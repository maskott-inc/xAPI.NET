using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xAPI.Client.Exceptions;
using xAPI.Client.Http;
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

            string url = this.BuildUrl(request);
            var document = new ActivityProfileDocument();
            await this._client.GetJsonDocument(url, new GetJsonDocumentOptions(), document);
            return document;
        }

        async Task<ActivityProfileDocument<T>> IActivityProfilesApi.Get<T>(GetActivityProfileRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            string url = this.BuildUrl(request);
            var document = Activator.CreateInstance<ActivityProfileDocument<T>>();
            await this._client.GetJsonDocument(url, new GetJsonDocumentOptions(), document);
            return document;
        }

        async Task<bool> IActivityProfilesApi.Put<T>(PutActivityProfileRequest<T> request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            string url = this.BuildUrl(request);

            try
            {
                await this._client.PutJsonDocument(url, new PutJsonDocumentOptions(), request.ActivityProfile);
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

            string url = this.BuildUrl(request);

            try
            {
                await this._client.PostJsonDocument(url, new PostJsonDocumentOptions(), request.ActivityProfile);
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

        async Task<List<string>> IActivityProfilesApi.GetMany(GetActivityProfilesRequest request)
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

        private string BuildUrl(ASingleActivityProfileRequest request)
        {
            var builder = new StringBuilder(ENDPOINT);
            builder.AppendFormat("?activityId={0}", Uri.EscapeDataString(request.ActivityId.ToString()));
            builder.AppendFormat("&profileId={0}", Uri.EscapeDataString(request.ProfileId));

            return builder.ToString();
        }

        private string BuildUrl(GetActivityProfilesRequest request)
        {
            var builder = new StringBuilder(ENDPOINT);
            builder.AppendFormat("?activityId={0}", Uri.EscapeDataString(request.ActivityId.ToString()));
            if (request.Since.HasValue)
            {
                builder.AppendFormat("&since={0}", Uri.EscapeDataString(request.Since.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")));
            }

            return builder.ToString();
        }

        #endregion
    }
}
