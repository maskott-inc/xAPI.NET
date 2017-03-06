using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class Activity : Activity<dynamic>
    {
    }

    public class Activity<T> : ObjectResource
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("definition")]
        public ActivityDefinition<T> Definition { get; set; }

        protected override string GetObjectType() { return "Activity"; }
    }
}
