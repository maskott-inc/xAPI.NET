using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace xAPI.Client.Resources
{
    public class AccountObject
    {
        [JsonProperty("homePage", Required = Required.Always)]
        [Required]
        public Uri HomePage { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        [Required]
        public string Name { get; set; }
    }
}
