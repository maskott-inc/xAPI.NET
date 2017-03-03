using Maskott.xAPI.Client.Resources.Metadata;
using Newtonsoft.Json;
using System;

namespace Maskott.xAPI.Client.Resources
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
    }
}
