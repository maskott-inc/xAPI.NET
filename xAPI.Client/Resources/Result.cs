using Newtonsoft.Json;
using System;
using xAPI.Client.Json;

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
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Duration { get; set; }

        [JsonProperty("extensions", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Extensions Extensions { get; set; }
    }
}
