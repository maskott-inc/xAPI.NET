using System;

namespace xAPI.Client.Requests
{
    public class GetStatesRequest : AStateRequest
    {
        public DateTimeOffset? Since { get; set; }
    }
}
