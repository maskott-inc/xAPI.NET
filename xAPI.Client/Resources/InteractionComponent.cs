using Newtonsoft.Json;

namespace xAPI.Client.Resources
{
    public class InteractionComponent
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public LanguageMap Description { get; set; }
    }
}
