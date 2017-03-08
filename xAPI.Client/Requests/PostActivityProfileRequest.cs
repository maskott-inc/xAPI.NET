using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public static class PostActivityProfileRequest
    {
        public static PostActivityProfileRequest<T> Create<T>(ActivityProfileDocument<T> activityProfile)
        {
            return new PostActivityProfileRequest<T>(activityProfile);
        }
    }

    public class PostActivityProfileRequest<T> : ARequest
    {
        internal PostActivityProfileRequest(ActivityProfileDocument<T> activityProfile)
        {
        }

        internal override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
