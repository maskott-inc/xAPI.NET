using Newtonsoft.Json;
using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    public class ContextActivities
    {
        [JsonProperty("parent")]
        public List<Activity> Parent { get; set; }

        [JsonProperty("grouping")]
        public List<Activity> Grouping { get; set; }

        [JsonProperty("category")]
        public List<Activity> Category { get; set; }

        [JsonProperty("other")]
        public List<Activity> Other { get; set; }
    }
}
