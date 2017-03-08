using System;

namespace xAPI.Client.Requests
{
    public class GetActivityProfilesRequest : AActivityProfileRequest
    {
        public DateTimeOffset? Since { get; set; }
    }
}
