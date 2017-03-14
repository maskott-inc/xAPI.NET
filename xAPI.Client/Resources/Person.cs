using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    public class Person : IObjectResource
    {
        [JsonProperty("name")]
        public List<string> Name { get; set; }

        [JsonProperty("mbox")]
        public List<Uri> MBox { get; set; }

        [JsonProperty("mbox_sha1sum")]
        public List<string> MBoxSHA1Sum { get; set; }

        [JsonProperty("openid")]
        public List<Uri> OpenId { get; set; }

        [JsonProperty("account")]
        public List<AccountObject> Account { get; set; }

        public string ObjectType => "Person";

    }
}
