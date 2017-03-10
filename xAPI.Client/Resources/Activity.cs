using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class Activity : ObjectResource
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("definition")]
        public ActivityDefinition Definition { get; set; }

        protected override string GetObjectType() { return "Activity"; }
    }
}
