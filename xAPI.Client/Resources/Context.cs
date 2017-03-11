using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class Context
    {
        [JsonProperty("registration", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Guid? Registration { get; set; }

        [JsonProperty("instructor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Agent Instructor { get; set; }

        [JsonProperty("team", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Group Team { get; set; }

        [JsonProperty("contextActivities", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ContextActivities ContextActivities { get; set; }

        [JsonProperty("revision", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Revision { get; set; }

        [JsonProperty("platform", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Platform { get; set; }

        [JsonProperty("language", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty("statement", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public StatementRef Statement { get; set; }

        [JsonProperty("extensions", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Extensions Extensions { get; set; }
    }
}
