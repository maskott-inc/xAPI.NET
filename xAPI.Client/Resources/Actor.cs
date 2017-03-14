using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public abstract class Actor : IStatementTarget, ISubStatementTarget
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("mbox")]
        public Uri MBox { get; set; }

        [JsonProperty("mbox_sha1sum")]
        public string MBoxSHA1Sum { get; set; }

        [JsonProperty("openid")]
        public Uri OpenId { get; set; }

        [JsonProperty("account")]
        public AccountObject Account { get; set; }

        public abstract string ObjectType { get; }
    }
}
