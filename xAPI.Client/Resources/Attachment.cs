using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class Attachment
    {
        [JsonProperty("usageType", Required = Required.Always)]
        public Uri UsageType { get; set; }

        [JsonProperty("display", Required = Required.Always)]
        public LanguageMap Display { get; set; }

        [JsonProperty("description")]
        public LanguageMap Description { get; set; }

        [JsonProperty("contentType", Required = Required.Always)]
        public string ContentType { get; set; }

        [JsonProperty("length", Required = Required.Always)]
        public int Length { get; set; }

        [JsonProperty("sha2", Required = Required.Always)]
        public string SHA2 { get; set; }

        [JsonProperty("fileUrl")]
        public Uri FileUrl { get; set; }
    }
}
