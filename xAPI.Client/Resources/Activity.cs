using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class Activity : IObjectResource
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("definition")]
        public ActivityDefinition Definition { get; set; }

        public string ObjectType => "Activity";
    }
}
