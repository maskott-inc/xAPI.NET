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

        [JsonProperty("choices")]
        public List<InteractionComponent> Choices { get; set; }

        [JsonProperty("scale")]
        public List<InteractionComponent> Scale { get; set; }

        [JsonProperty("source")]
        public List<InteractionComponent> Source { get; set; }

        [JsonProperty("target")]
        public List<InteractionComponent> Target { get; set; }

        [JsonProperty("steps")]
        public List<InteractionComponent> Steps { get; set; }

        [JsonProperty("extensions")]
        public Extensions Extensions { get; set; }
    }
}
