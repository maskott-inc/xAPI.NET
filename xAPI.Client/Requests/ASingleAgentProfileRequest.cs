using System;

namespace xAPI.Client.Requests
{
    public class ASingleAgentProfileRequest : AAgentProfileRequest
    {
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
