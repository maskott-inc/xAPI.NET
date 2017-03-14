using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class Context
    {
        [JsonProperty("registration")]
        public Guid? Registration { get; set; }

        [JsonProperty("instructor")]
        public Agent Instructor { get; set; }

        [JsonProperty("team")]
        public Group Team { get; set; }

        [JsonProperty("contextActivities")]
        public ContextActivities ContextActivities { get; set; }

        [JsonProperty("revision")]
        public string Revision { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("statement")]
        public StatementRef Statement { get; set; }

        [JsonProperty("extensions")]
        public Extensions Extensions { get; set; }
    }
}
