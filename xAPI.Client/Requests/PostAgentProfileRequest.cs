using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public static class PostAgentProfileRequest
    {
        public static PostAgentProfileRequest<T> Create<T>(AgentProfileDocument<T> agentProfile)
        {
            return new PostAgentProfileRequest<T>(agentProfile);
        }
    }

    public class PostAgentProfileRequest<T> : ASingleAgentProfileRequest
    {
        public AgentProfileDocument<T> AgentProfile { get; private set; }

        internal PostAgentProfileRequest(AgentProfileDocument<T> agentProfile)
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
