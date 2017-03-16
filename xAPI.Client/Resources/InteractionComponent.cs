using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    public class InteractionComponent
    {
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public string Id { get; set; }

        [JsonProperty("description")]
        [ValidateProperty]
        public LanguageMap Description { get; set; }
    }
}
