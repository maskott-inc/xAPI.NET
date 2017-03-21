using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// The base class for any agent profile-related request.
    /// </summary>
    public class AAgentProfileRequest : ARequest
    {
        /// <summary>
        /// The Agent associated with this Profile document.
        /// </summary>
        public Agent Agent { get; set; }

        internal override void Validate()
        {
            if (this.Agent == null)
            {
                throw new ArgumentNullException(nameof(this.Agent));
            }
        }
    }
}
