using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    public class ActivityDefinition
    {
        [JsonProperty("name")]
        public LanguageMap Name { get; set; }

        [JsonProperty("description")]
        public LanguageMap Description { get; set; }

        [JsonProperty("type")]
        public Uri Type { get; set; }

        [JsonProperty("moreInfo")]
        public Uri MoreInfo { get; set; }

        [JsonProperty("interactionType")]
        public string InteractionType { get; set; }

        [JsonProperty("correctResponsesPattern")]
        public List<string> CorrectResponsesPattern { get; set; }

        [JsonProperty("choices", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Interaction> Choices { get; set; }

        [JsonProperty("scale", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Interaction> Scale { get; set; }

        [JsonProperty("source", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Interaction> Source { get; set; }

        [JsonProperty("target", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Interaction> Target { get; set; }

        [JsonProperty("steps", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Interaction> Steps { get; set; }

        [JsonProperty("extensions")]
        public Dictionary<Uri, dynamic> Extensions { get; set; }
    }
}
