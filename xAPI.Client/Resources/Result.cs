using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class Result
    {
        [JsonProperty("score", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Score Score { get; set; }

        [JsonProperty("success", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Success { get; set; }

        [JsonProperty("completion", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Completion { get; set; }

        [JsonProperty("response", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Response { get; set; }

        [JsonProperty("duration", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TimeSpan Duration { get; set; }

        [JsonProperty("extensions", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Extensions Extensions { get; set; }
    }
}
