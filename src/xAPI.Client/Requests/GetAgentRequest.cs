using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Class used to perform a GET request on the agents resource.
    /// </summary>
    public class GetAgentRequest : ARequest
    {
        /// <summary>
        /// Creates a new instance of GetAgentRequest.
        /// </summary>
        public GetAgentRequest()
        {
        }

        /// <summary>
        /// Creates a new instance of GetAgentRequest using the
        /// specified agent to initialize its state.
        /// </summary>
        /// <param name="agent">The agent.</param>
        public GetAgentRequest(Agent agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent));
            }

            this.Agent = agent;
        }

        /// <summary>
        /// Gets or sets the agent.
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
