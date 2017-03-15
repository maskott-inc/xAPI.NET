using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    public class StatementResult
    {
        [JsonProperty("statements", Required = Required.Always)]
        public List<Statement> Statements { get; set; }

        [JsonProperty("more")]
        public Uri More { get; set; }

        [JsonIgnore]
        public DateTimeOffset ConsistentThrough { get; set; }
    }
}
