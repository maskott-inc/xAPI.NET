using System;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints.Impl
{
    internal class ActivitiesApi : IActivitiesApi
    {
        private const string ENDPOINT = "activities";
        private readonly IHttpClientWrapper _client;

        public ActivitiesApi(IHttpClientWrapper client)
        {
            this._client = client;
        }

        #region IActivitiesApi members

        Task<Activity> IActivitiesApi.Get(GetActivityRequest request)
        {
            return this.Get<Activity>(request);
        }

        Task<Activity<T>> IActivitiesApi.Get<T>(GetActivityRequest request)
        {
            return this.Get<Activity<T>>(request);
        }

        #endregion

        #region Utils

        private Task<T> Get<T>(GetActivityRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.ActivityId == null)
            {
                throw new ArgumentNullException(nameof(request.ActivityId));
            }
            if (!request.ActivityId.IsAbsoluteUri)
            {
                throw new ArgumentException("IRI should be absolute", nameof(request.ActivityId));
            }

            string url = string.Format("{0}?activityId={1}", ENDPOINT, Uri.EscapeDataString(request.ActivityId.ToString()));
            return this._client.GetJson<T>(url);
        }

        #endregion
    }
}
