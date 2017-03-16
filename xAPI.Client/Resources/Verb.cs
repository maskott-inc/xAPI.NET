using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace xAPI.Client.Resources
{
    public class Verb
    {
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public Uri Id { get; set; }

        [JsonProperty("display")]
        public LanguageMap Display { get; set; }
    }
}
