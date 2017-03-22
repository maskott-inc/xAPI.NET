using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// Activity metadata.
    /// </summary>
    public class ActivityDefinition
    {
        /// <summary>
        /// The human readable/visual name of the Activity.
        /// </summary>
        [JsonProperty("name")]
        [ValidateProperty]
        public LanguageMap Name { get; set; }

        /// <summary>
        /// A description of the Activity.
        /// </summary>
        [JsonProperty("description")]
        [ValidateProperty]
        public LanguageMap Description { get; set; }

        /// <summary>
        /// The type of Activity.
        /// </summary>
        [JsonProperty("type")]
        public Uri Type { get; set; }

        /// <summary>
        /// Resolves to a document with human-readable information about the
        /// Activity, which could include a way to launch the activity. 
        /// </summary>
        [JsonProperty("moreInfo")]
        public Uri MoreInfo { get; set; }

        /// <summary>
        /// The type of interaction.
        /// </summary>
        [JsonProperty("interactionType")]
        public InteractionType? InteractionType { get; set; }

        /// <summary>
        /// A pattern representing the correct response to the interaction.
        /// The structure of this pattern varies depending on the
        /// interactionType.
        /// </summary>
        [JsonProperty("correctResponsesPattern")]
        public List<string> CorrectResponsesPattern { get; set; }

        /// <summary>
        /// Specific to the given interactionType.
        /// </summary>
        [JsonProperty("choices")]
        [ValidateProperty]
        public List<InteractionComponent> Choices { get; set; }

        /// <summary>
        /// Specific to the given interactionType.
        /// </summary>
        [JsonProperty("scale")]
        [ValidateProperty]
        public List<InteractionComponent> Scale { get; set; }

        /// <summary>
        /// Specific to the given interactionType.
        /// </summary>
        [JsonProperty("source")]
        [ValidateProperty]
        public List<InteractionComponent> Source { get; set; }

        /// <summary>
        /// Specific to the given interactionType.
        /// </summary>
        [JsonProperty("target")]
        [ValidateProperty]
        public List<InteractionComponent> Target { get; set; }

        /// <summary>
        /// Specific to the given interactionType.
        /// </summary>
        [JsonProperty("steps")]
        [ValidateProperty]
        public List<InteractionComponent> Steps { get; set; }

        /// <summary>
        /// A map of other properties as needed.
        /// </summary>
        [JsonProperty("extensions")]
        [ValidateProperty]
        public Extensions Extensions { get; set; }
    }
}
