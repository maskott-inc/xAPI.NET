using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        Task<ActivityProfileDocument> IActivityProfilesApi.Get(GetActivityProfileRequest request)
        {
            throw new NotImplementedException();
        }

        Task IActivityProfilesApi.Put(PutActivityProfileRequest request, ActivityProfileDocument activityProfile)
        {
            throw new NotImplementedException();
        }

        Task IActivityProfilesApi.Post(PostActivityProfileRequest request, ActivityProfileDocument activityProfile)
        {
            throw new NotImplementedException();
        }

        Task IActivityProfilesApi.Delete(DeleteActivityProfileRequest request)
        {
            throw new NotImplementedException();
        }

        Task<List<string>> IActivityProfilesApi.GetMany(GetActivityProfilesRequest request)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
