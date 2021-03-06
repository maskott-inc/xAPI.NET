﻿using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Factory used to create requests to perform a POST request on the
    /// agent profiles resource.
    /// </summary>
    public static class PostAgentProfileRequest
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
        public static PostAgentProfileRequest<T> Create<T>(AgentProfileDocument<T> agentProfile)
        {
            return new PostAgentProfileRequest<T>(agentProfile);
        }
    }

    /// <summary>
    /// Class used to perform a POST request on the agent profiles resource.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the document to be stored as JSON in the LRS.
    /// The type must support JSON serialization.
    /// </typeparam>
    public class PostAgentProfileRequest<T> : ASingleAgentProfileRequest
    {
        internal AgentProfileDocument<T> AgentProfile { get; private set; }

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
