using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    public class Attachment
    {
        [JsonProperty("usageType", Required = Required.Always)]
        [Required]
        public Uri UsageType { get; set; }

        [JsonProperty("display", Required = Required.Always)]
        [Required, ValidateProperty]
        public LanguageMap Display { get; set; }

        [JsonProperty("description")]
        [ValidateProperty]
        public LanguageMap Description { get; set; }

        [JsonProperty("contentType", Required = Required.Always)]
        [Required]
        public string ContentType { get; set; }

        [JsonProperty("length", Required = Required.Always)]
        public uint Length { get; set; }

        [JsonProperty("sha2", Required = Required.Always)]
        [Required]
        public string SHA2 { get; set; }

        [JsonProperty("fileUrl")]
        public Uri FileUrl { get; set; }
    }
}
