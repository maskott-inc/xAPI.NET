using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    public class Activity : IStatementTarget, ISubStatementTarget
    {
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public Uri Id { get; set; }

        [JsonProperty("definition")]
        [Required, ValidateProperty]
        public ActivityDefinition Definition { get; set; }

        public string ObjectType => "Activity";
    }
}
