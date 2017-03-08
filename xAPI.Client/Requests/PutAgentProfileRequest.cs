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

    public class PutAgentProfileRequest<T> : ARequest
    {
        internal PutAgentProfileRequest(AgentProfileDocument<T> agentProfile)
        {
        }

        internal override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
