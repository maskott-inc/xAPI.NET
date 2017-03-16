using Newtonsoft.Json;
using System.Collections.Generic;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    public class ContextActivities
    {
        [JsonProperty("parent")]
        [ValidateProperty]
        public List<Activity> Parent { get; set; }

        [JsonProperty("grouping")]
        [ValidateProperty]
        public List<Activity> Grouping { get; set; }

        [JsonProperty("category")]
        [ValidateProperty]
        public List<Activity> Category { get; set; }

        [JsonProperty("other")]
        [ValidateProperty]
        public List<Activity> Other { get; set; }
    }
}
