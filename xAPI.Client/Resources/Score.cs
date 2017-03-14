using Newtonsoft.Json;

namespace xAPI.Client.Resources
{
    public class Score
    {
        [JsonProperty("scaled")]
        public decimal? Scaled { get; set; }

        [JsonProperty("raw")]
        public decimal? Raw { get; set; }

        [JsonProperty("min")]
        public decimal? Min { get; set; }

        [JsonProperty("max")]
        public decimal? Max { get; set; }
    }
}
