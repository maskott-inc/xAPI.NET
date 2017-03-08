using System;
using System.Threading.Tasks;
using xAPI.Client.Exceptions;
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

        async Task<Activity> IActivitiesApi.Get(GetActivityRequest request)
        {
            return await this.Get<Activity>(request);
        }

        async Task<Activity<T>> IActivitiesApi.Get<T>(GetActivityRequest request)
        {
            return await this.Get<Activity<T>>(request);
        }

        #endregion

        #region Utils

        private async Task<T> Get<T>(GetActivityRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            string url = string.Format("{0}?activityId={1}", ENDPOINT, Uri.EscapeDataString(request.ActivityId.ToString()));
            try
            {
                return await this._client.GetJson<T>(url);
            }
            catch (NotFoundException)
            {
                return default(T);
            }
        }

        #endregion
    }
}
