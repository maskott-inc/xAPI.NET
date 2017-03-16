using Newtonsoft.Json;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    public class Result
    {
        [JsonProperty("score")]
        [ValidateProperty]
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
        [ValidateProperty]
        public Extensions Extensions { get; set; }
    }
}
