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

    public class PostAgentProfileRequest<T> : ARequest
    {
        internal PostAgentProfileRequest(AgentProfileDocument<T> agentProfile)
        {
        }

        internal override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
