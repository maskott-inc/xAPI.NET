using System;
using System.Threading.Tasks;
using xAPI.Client.Exceptions;
using xAPI.Client.Http;
using xAPI.Client.Http.Options;
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
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT);
            options.QueryStringParameters.Add("activityId", request.ActivityId.ToString());

            try
            {
                HttpResult<Activity> result = await this._client.GetJson<Activity>(options);
                return result.Content;
            }
            catch (NotFoundException)
            {
                return null;
            }
        }

        #endregion
    }
}
