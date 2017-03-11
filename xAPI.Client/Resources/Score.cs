using Newtonsoft.Json;

namespace xAPI.Client.Resources
{
    public class Score
    {
        [JsonProperty("scaled", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? Scaled { get; set; }

        [JsonProperty("raw", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? Raw { get; set; }

        [JsonProperty("min", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? Min { get; set; }

        [JsonProperty("max", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? Max { get; set; }
    }
}
