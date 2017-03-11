using Newtonsoft.Json;
using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    public class Group : Actor
    {
        [JsonProperty("member")]
        public List<Agent> Members { get; set; }

        public override string ObjectType => "Group";
    }
}
