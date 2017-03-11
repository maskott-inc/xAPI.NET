using Newtonsoft.Json;
using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    public class About
    {
        [JsonProperty("version")]
        public List<XApiVersion> Versions { get; set; }

        [JsonProperty("extensions")]
        public Extensions Extensions { get; set; }
    }
}
