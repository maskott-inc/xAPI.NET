using Newtonsoft.Json;
using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    public class ContextActivities
    {
        [JsonProperty("parent", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Activity> Parent { get; set; }

        [JsonProperty("grouping", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Activity> Grouping { get; set; }

        [JsonProperty("category", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Activity> Category { get; set; }

        [JsonProperty("other", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Activity> Other { get; set; }
    }
}
