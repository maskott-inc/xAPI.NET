using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class Agent : IObjectResource
    {
        public string ObjectType => "Agent";

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("mbox", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Uri MBox { get; set; }

        [JsonProperty("mbox_sha1sum", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string MBoxSHA1Sum { get; set; }

        [JsonProperty("openid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Uri OpenId { get; set; }

        [JsonProperty("account", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public AccountObject Account { get; set; }
    }
}
