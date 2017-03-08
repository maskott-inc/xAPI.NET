using System;

namespace xAPI.Client.Requests
{
    public class AActivityProfileRequest : ARequest
    {
        public Uri ActivityId { get; set; }

        internal override void Validate()
        {
            if (this.ActivityId == null)
            {
                throw new ArgumentNullException(nameof(this.ActivityId));
            }
        }
    }
}
