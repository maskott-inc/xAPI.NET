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

        [JsonProperty("result")]
        public Result Result { get; set; }

        [JsonProperty("context")]
        public Context Context { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("stored")]
        public DateTimeOffset Stored { get; set; }

        [JsonProperty("authority")]
        [JsonConverter(typeof(ObjectResourceConverter<Agent>))]
        public Actor Authority { get; set; }

        [JsonProperty("version")]
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
