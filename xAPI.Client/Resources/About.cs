using Newtonsoft.Json;
using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// Object containing information about this LRS, including the
    /// xAPI version supported.
    /// </summary>
    public class About
    {
        /// <summary>
        /// xAPI versions this LRS supports.
        /// </summary>
        [JsonProperty("version")]
        public List<XApiVersion> Versions { get; set; }

        /// <summary>
        /// A map of other properties as needed.
        /// </summary>
        [JsonProperty("extensions")]
        public Extensions Extensions { get; set; }
    }
}
