using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public class AAgentProfileRequest : ARequest
    {
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
