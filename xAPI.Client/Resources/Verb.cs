using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class Verb
    {
        [JsonProperty("id", Required = Required.Always)]
        public Uri Id { get; set; }

        [JsonProperty("display")]
        public LanguageMap Display { get; set; }
    }
}
