using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Class used to GET multiple statements from the LRS.
    /// </summary>
    public class GetStatementsRequest : AGetStatementRequest
    {
        /// <summary>
        /// Filter, only return Statements for which the specified
        /// Agent or Group is the Actor or Object of the Statement.
        /// </summary>
        public Agent Agent { get; set; }

        /// <summary>
        /// Filter, only return Statements matching the specified
        /// Verb id.
        /// </summary>
        public Uri Verb { get; set; }

        /// <summary>
        /// Filter, only return Statements for which the Object of
        /// the Statement is an Activity with the specified id.
        /// </summary>
        public Uri ActivityId { get; set; }

        /// <summary>
        /// Filter, only return Statements matching the specified
        /// registration id. Note that although frequently a unique
        /// registration will be used for one Actor assigned to one
        /// Activity, this cannot be assumed. If only Statements for
        /// a certain Actor or Activity are required, those
        /// parameters also need to be specified.
        /// </summary>
        public Guid? Registration { get; set; }

        /// <summary>
        /// Apply the Activity filter broadly. Include Statements for
        /// which the Object, any of the context Activities, or any of
        /// those properties in a contained SubStatement match the
        /// Activity parameter, instead of that parameter's normal
        /// behavior. Matching is defined in the same way it is for
        /// the "activity" parameter.
        /// </summary>
        public bool RelatedActivities { get; set; }

        /// <summary>
        /// Apply the Agent filter broadly. Include Statements for which
        /// the Actor, Object, Authority, Instructor, Team, or any of
        /// these properties in a contained SubStatement match the Agent
        /// parameter, instead of that parameter's normal behavior.
        /// Matching is defined in the same way it is for the "agent"
        /// parameter.
        /// </summary>
        public bool RelatedAgents { get; set; }

        /// <summary>
        /// Only Statements stored since the specified Timestamp (exclusive)
        /// are returned.
        /// </summary>
        public DateTimeOffset? Since { get; set; }

        /// <summary>
        /// Only Statements stored at or before the specified Timestamp are
        /// returned.
        /// </summary>
        public DateTimeOffset? Until { get; set; }

        /// <summary>
        /// Maximum number of Statements to return. 0 indicates return the
        /// maximum the server will allow.
        /// </summary>
        public uint Limit { get; set; }

        /// <summary>
        /// If true, return results in ascending order of stored time.
        /// </summary>
        public bool Ascending { get; set; }

        internal override void Validate()
        {
            base.Validate();
        }
    }
}
