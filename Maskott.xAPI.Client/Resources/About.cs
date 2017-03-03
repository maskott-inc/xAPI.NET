using Maskott.xAPI.Client.Resources;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Maskott.xAPI.Client.Resources
{
    public class About : About<dynamic>
    {
    }

    public class About<T>
    {
        [JsonProperty("version")]
        public List<XApiVersion> Versions { get; set; }

        [JsonProperty("extensions")]
        public T Extensions { get; set; }
    }
}
