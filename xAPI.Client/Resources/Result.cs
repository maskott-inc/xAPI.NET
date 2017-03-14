using Newtonsoft.Json;

namespace xAPI.Client.Resources
{
    public class Result
    {
        [JsonProperty("score")]
        public Score Score { get; set; }

        [JsonProperty("success")]
        public bool? Success { get; set; }

        [JsonProperty("completion")]
        public bool? Completion { get; set; }

        [JsonProperty("response")]
        public string Response { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("extensions")]
        public Extensions Extensions { get; set; }
    }
}
