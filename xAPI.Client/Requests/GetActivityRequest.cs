using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public class GetActivityRequest
    {
        public GetActivityRequest()
        {
        }

        public GetActivityRequest(Activity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException(nameof(activity));
            }

            this.ActivityId = activity.Id;
        }

        public Uri ActivityId { get; set; }
    }
}
