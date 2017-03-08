using System;

namespace xAPI.Client.Requests
{
    public class GetAgentProfilesRequest : AAgentProfileRequest
    {
        public DateTimeOffset? Since { get; set; }
    }
}
