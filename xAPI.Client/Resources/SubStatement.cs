using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using xAPI.Client.Json;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    public class SubStatement : IStatementTarget
    {
        [JsonProperty("actor", Required = Required.Always)]
        [JsonConverter(typeof(ObjectResourceConverter<Agent>))]
        [Required, ValidateProperty]
        public Actor Actor { get; set; }

        [JsonProperty("verb", Required = Required.Always)]
        [Required, ValidateProperty]
        public Verb Verb { get; set; }

        [JsonProperty("object", Required = Required.Always)]
        [JsonConverter(typeof(ObjectResourceConverter<Activity>))]
        [Required, ValidateProperty]
        public ISubStatementTarget Object { get; set; }

        [JsonProperty("result")]
        [ValidateProperty]
        public Result Result { get; set; }

        [JsonProperty("context")]
        [ValidateProperty]
        public Context Context { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("attachments")]
        [ValidateProperty]
        public List<Attachment> Attachments { get; set; }

        public string ObjectType => "SubStatement";
    }
}
