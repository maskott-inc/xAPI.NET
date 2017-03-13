using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public class GetStatementsRequest : AGetStatementRequest
    {
        public Agent Agent { get; set; }

        public Uri Verb { get; set; }

        public Uri ActivityId { get; set; }

        public Guid? Registration { get; set; }

        public bool RelatedActivities { get; set; }

        public bool RelatedAgents { get; set; }

        public DateTimeOffset? Since { get; set; }

        public DateTimeOffset? Until { get; set; }

        public uint Limit { get; set; }

        public bool Ascending { get; set; }

        internal override void Validate()
        {
            base.Validate();
        }
    }
}
