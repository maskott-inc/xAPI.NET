using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public static class PutAgentProfileRequest
    {
        public static PutAgentProfileRequest<T> Create<T>(AgentProfileDocument<T> agentProfile)
        {
            return new PutAgentProfileRequest<T>(agentProfile);
        }
    }

    public class PutAgentProfileRequest<T> : ASingleAgentProfileRequest
    {
        public AgentProfileDocument<T> AgentProfile { get; private set; }

        internal PutAgentProfileRequest(AgentProfileDocument<T> agentProfile)
        {
            this.AgentProfile = agentProfile;
        }

        internal override void Validate()
        {
            base.Validate();

            if (this.AgentProfile == null)
            {
                throw new ArgumentNullException(nameof(this.AgentProfile));
            }
        }
    }
}
