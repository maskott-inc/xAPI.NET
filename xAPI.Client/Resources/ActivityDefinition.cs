using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    public class ActivityDefinition
    {
        [JsonProperty("name")]
        [ValidateProperty]
        public LanguageMap Name { get; set; }

        [JsonProperty("description")]
        [ValidateProperty]
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
        [ValidateProperty]
        public List<InteractionComponent> Choices { get; set; }

        [JsonProperty("scale")]
        [ValidateProperty]
        public List<InteractionComponent> Scale { get; set; }

        [JsonProperty("source")]
        [ValidateProperty]
        public List<InteractionComponent> Source { get; set; }

        [JsonProperty("target")]
        [ValidateProperty]
        public List<InteractionComponent> Target { get; set; }

        [JsonProperty("steps")]
        [ValidateProperty]
        public List<InteractionComponent> Steps { get; set; }

        [JsonProperty("extensions")]
        [ValidateProperty]
        public Extensions Extensions { get; set; }
    }
}
