using System;
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
    internal class ActivitiesApi : IActivitiesApi
    {
        private const string ENDPOINT = "activities";
        private readonly IHttpClientWrapper _client;

        public ActivitiesApi(IHttpClientWrapper client)
        {
            this._client = client;
        }

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
                HttpResponseMessage response = await this._client.GetJson(options);
                return await response.Content.ReadAsAsync<Activity>(new[] { new StrictJsonMediaTypeFormatter() });
            }
            catch (NotFoundException)
            {
                return null;
            }
        }
    }
}
