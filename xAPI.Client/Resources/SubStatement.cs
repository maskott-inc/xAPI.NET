using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using xAPI.Client.Json;

namespace xAPI.Client.Resources
{
    public class SubStatement : IStatementTarget
    {
        [JsonProperty("actor")]
        [JsonConverter(typeof(ObjectResourceConverter<Agent>))]
        public Actor Actor { get; set; }

        [JsonProperty("verb")]
        public Verb Verb { get; set; }

        [JsonProperty("object")]
        [JsonConverter(typeof(ObjectResourceConverter<Activity>))]
        public ISubStatementTarget Object { get; set; }

        [JsonProperty("result", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Result Result { get; set; }

        [JsonProperty("context", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Context Context { get; set; }

        [JsonProperty("timestamp", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("attachments", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Attachment> Attachments { get; set; }

        public string ObjectType => "SubStatement";
    }
}
