using Maskott.xAPI.Client.Resources.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Maskott.xAPI.Client.Resources
{
    public class Person : ObjectResource
    {
        [JsonProperty("id")]
        public List<string> Name { get; set; }

        [JsonProperty("mbox", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Uri> MBox { get; set; }

        [JsonProperty("mbox_sha1sum", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> MBoxSHA1Sum { get; set; }

        [JsonProperty("openid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Uri> OpenId { get; set; }

        [JsonProperty("account", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<AccountObject> Account { get; set; }
    }
}
