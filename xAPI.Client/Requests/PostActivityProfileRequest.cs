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

    public class PostActivityProfileRequest<T> : ASingleActivityProfileRequest
    {
        public ActivityProfileDocument<T> ActivityProfile { get; private set; }

        internal PostActivityProfileRequest(ActivityProfileDocument<T> activityProfile)
        {
            this.ActivityProfile = activityProfile;
        }

        internal override void Validate()
        {
            base.Validate();

            if (this.ActivityProfile == null)
            {
                throw new ArgumentNullException(nameof(this.ActivityProfile));
            }
        }
    }
}
