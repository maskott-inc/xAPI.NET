using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class Verb
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("display")]
        public LanguageMap Display { get; set; }
    }
}
