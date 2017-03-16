using Newtonsoft.Json;
using System.Collections.Generic;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    public class Group : Actor
    {
        [JsonProperty("member")]
        [ValidateProperty]
        public List<Agent> Members { get; set; }

        public override string ObjectType => "Group";
    }
}
