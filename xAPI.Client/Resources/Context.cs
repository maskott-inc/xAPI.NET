using Newtonsoft.Json;
using System;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    public class Context
    {
        [JsonProperty("registration")]
        public Guid? Registration { get; set; }

        [JsonProperty("instructor")]
        [ValidateProperty]
        public Agent Instructor { get; set; }

        [JsonProperty("team")]
        [ValidateProperty]
        public Group Team { get; set; }

        [JsonProperty("contextActivities")]
        [ValidateProperty]
        public ContextActivities ContextActivities { get; set; }

        [JsonProperty("revision")]
        public string Revision { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("statement")]
        [ValidateProperty]
        public StatementRef Statement { get; set; }

        [JsonProperty("extensions")]
        [ValidateProperty]
        public Extensions Extensions { get; set; }
    }
}
