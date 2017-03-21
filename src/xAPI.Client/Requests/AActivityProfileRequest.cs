using System;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// The base class for any activity profile-related request.
    /// </summary>
    public class AActivityProfileRequest : ARequest
    {
        /// <summary>
        /// The Activity id associated with this Profile document.
        /// </summary>
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
