using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public static class PutActivityProfileRequest
    {
        public static PutActivityProfileRequest<T> Create<T>(ActivityProfileDocument<T> activityProfile)
        {
            return new PutActivityProfileRequest<T>(activityProfile);
        }
    }

    public class PutActivityProfileRequest<T> : ARequest
    {
        internal PutActivityProfileRequest(ActivityProfileDocument<T> activityProfile)
        {
        }

        internal override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
