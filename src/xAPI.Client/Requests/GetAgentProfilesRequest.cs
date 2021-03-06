﻿using System;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Class used to perform a GET request on the agent profiles resource
    /// for a multiple documents.
    /// </summary>
    public class GetAgentProfilesRequest : AAgentProfileRequest
    {
        /// <summary>
        /// Only ids of documents stored since the specified Timestamp (exclusive)
        /// are returned.
        /// </summary>
        public DateTimeOffset? Since { get; set; }
    }
}
