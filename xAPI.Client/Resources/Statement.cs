using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using xAPI.Client.Json;

namespace xAPI.Client.Resources
{
    public class Statement
    {
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id { get; set; }

        [JsonProperty("actor", Required = Required.Always)]
        [JsonConverter(typeof(ObjectResourceConverter<Agent>))]
        public Actor Actor { get; set; }

        [JsonProperty("verb", Required = Required.Always)]
        public Verb Verb { get; set; }

        [JsonProperty("object", Required = Required.Always)]
        [JsonConverter(typeof(ObjectResourceConverter<Activity>))]
        public IStatementTarget Object { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }

        [JsonProperty("context")]
        public Context Context { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("stored", Required = Required.Always)]
        public DateTimeOffset Stored { get; set; }

        [JsonProperty("authority", Required = Required.Always)]
        [JsonConverter(typeof(ObjectResourceConverter<Agent>))]
        public Actor Authority { get; set; }

        [JsonProperty("version", Required = Required.Always)]
        public XApiVersion Version { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        public Statement()
        {
            this.Id = Guid.NewGuid();
        }

        public static Statement CreateVoidingStatement(Guid id, Agent agent, Guid voidedStatementId)
        {
            return new Statement()
            {
                Id = id,
                Actor = agent,
                Verb = new Verb()
                {
                    Id = new Uri("http://adlnet.gov/expapi/verbs/voided"),
                    Display = new LanguageMap() { { "en-US", "voided" } }
                },
                Object = new StatementRef()
                {
                    Id = voidedStatementId
                }
            };
        }
    }
}
