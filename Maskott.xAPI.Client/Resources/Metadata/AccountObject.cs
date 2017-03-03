using Newtonsoft.Json;
using System;

namespace Maskott.xAPI.Client.Resources.Metadata
{
    public class AccountObject
    {
        [JsonProperty("homePage")]
        public Uri HomePage { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
