using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    public class StatementResult
    {
        [JsonProperty("statements")]
        public List<Statement> Statements { get; set; }

        [JsonProperty("more")]
        public Uri More { get; set; }
    }
}
