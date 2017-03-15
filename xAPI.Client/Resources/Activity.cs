using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class Activity : IStatementTarget, ISubStatementTarget
    {
        [JsonProperty("id", Required = Required.Always)]
        public Uri Id { get; set; }

        [JsonProperty("definition")]
        public ActivityDefinition Definition { get; set; }

        public string ObjectType => "Activity";
    }
}
