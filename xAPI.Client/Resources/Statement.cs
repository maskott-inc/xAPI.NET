using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using xAPI.Client.Json;

namespace xAPI.Client.Resources
{
    public class Statement
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("actor")]
        [JsonConverter(typeof(ObjectResourceConverter<Agent>))]
        public Actor Actor { get; set; }

        [JsonProperty("verb")]
        public Verb Verb { get; set; }

        [JsonProperty("object")]
        [JsonConverter(typeof(ObjectResourceConverter<Activity>))]
        public IStatementTarget Object { get; set; }

        [JsonProperty("result", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Result Result { get; set; }

        [JsonProperty("context", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Context Context { get; set; }

        [JsonProperty("timestamp", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("stored", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset Stored { get; set; }

        [JsonProperty("authority", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(ObjectResourceConverter<Agent>))]
        public Actor Authority { get; set; }

        [JsonProperty("version", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public XApiVersion Version { get; set; }

        [JsonProperty("attachments", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Attachment> Attachments { get; set; }

        public Statement()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
