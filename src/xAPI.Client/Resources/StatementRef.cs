using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// A Statement Reference is a pointer to another pre-existing Statement.
    /// </summary>
    public class StatementRef : IStatementTarget, ISubStatementTarget
    {
        /// <summary>
        /// The UUID of a Statement.
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id { get; set; }

        /// <summary>
        /// Always "StatementRef".
        /// </summary>
        public string ObjectType => "StatementRef";
    }
}
