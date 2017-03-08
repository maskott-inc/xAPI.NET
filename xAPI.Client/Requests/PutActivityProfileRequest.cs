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

    public class PutActivityProfileRequest<T> : ASingleActivityProfileRequest
    {
        public ActivityProfileDocument<T> ActivityProfile { get; private set; }

        internal PutActivityProfileRequest(ActivityProfileDocument<T> activityProfile)
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
