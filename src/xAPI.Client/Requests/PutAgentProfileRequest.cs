using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Factory used to create requests to perform a PUT request on the
    /// agent profiles resource.
    /// </summary>
    public static class PutAgentProfileRequest
    {
        /// <summary>
        /// Creates a new request instance using an existing document
        /// to handle concurrency operations.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the document to be stored as JSON in the LRS.
        /// The type must support JSON serialization.
        /// </typeparam>
        /// <param name="agentProfile">The document used for concurrency comparisons.</param>
        /// <returns></returns>
        public static PutAgentProfileRequest<T> Create<T>(AgentProfileDocument<T> agentProfile)
        {
            return new PutAgentProfileRequest<T>(agentProfile);
        }
    }

    /// <summary>
    /// Class used to perform a PUT request on the agent profiles resource.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the document to be stored as JSON in the LRS.
    /// The type must support JSON serialization.
    /// </typeparam>
    public class PutAgentProfileRequest<T> : ASingleAgentProfileRequest
    {
        /// <summary>
        /// The document used for concurrency comparisons.
        /// </summary>
        public AgentProfileDocument<T> AgentProfile { get; }

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
