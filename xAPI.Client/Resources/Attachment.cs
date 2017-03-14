using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class Attachment
    {
        [JsonProperty("usageType")]
        public Uri UsageType { get; set; }

        [JsonProperty("display")]
        public LanguageMap Display { get; set; }

        [JsonProperty("description")]
        public LanguageMap Description { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("sha2")]
        public string SHA2 { get; set; }

        [JsonProperty("fileUrl")]
        public Uri FileUrl { get; set; }
    }
}
