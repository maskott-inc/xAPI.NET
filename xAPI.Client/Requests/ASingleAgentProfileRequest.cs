using System;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// The base class for any agent profile-related request, when the request
    /// is associated with a single agent profile.
    /// </summary>
    public class ASingleAgentProfileRequest : AAgentProfileRequest
    {
        /// <summary>
        /// The profile id associated with this Profile document.
        /// </summary>
        public string ProfileId { get; set; }

        internal override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(this.ProfileId))
            {
                throw new ArgumentNullException(nameof(this.ProfileId));
            }
        }
    }
}
