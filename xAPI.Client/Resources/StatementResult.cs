using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// A collection of statements representing all or part of a GET statements
    /// request.
    /// </summary>
    public class StatementResult
    {
        /// <summary>
        /// List of Statements. If the list returned has been limited (due to pagination),
        /// and there are more results, they will be located at the "statements" property
        /// within the container located at the IRL provided by the "more" property of
        /// this Statement result Object.
        /// Where no matching Statements are found, this property will contain an empty
        /// array.
        /// </summary>
        [JsonProperty("statements", Required = Required.Always)]
        public List<Statement> Statements { get; set; }

        /// <summary>
        /// Relative IRL that can be used to fetch more results, including the full path
        /// and optionally a query string but excluding scheme, host, and port.
        /// Empty string if there are no more results to fetch.
        /// </summary>
        [JsonProperty("more")]
        public Uri More { get; set; }

        /// <summary>
        /// The timestamp for which all Statements that have or will have a "stored"
        /// property before that time are known with reasonable certainty to be available
        /// for retrieval. This time SHOULD take into account any temporary condition,
        /// such as excessive load, which might cause a delay in Statements becoming
        /// available for retrieval. It is expected that this will be a recent timestamp,
        /// even if there are no recently received Statements.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset ConsistentThrough { get; set; }
    }
}
