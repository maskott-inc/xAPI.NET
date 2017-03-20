using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// Depending on interactionType, Interaction Activities can take
    /// additional properties, each containing a list of interaction
    /// components. These additional properties are called "interaction
    /// component lists".
    /// </summary>
    public class InteractionComponent
    {
        /// <summary>
        /// Identifies the interaction component within the list.
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// A description of the interaction component (for example,
        /// the text for a given choice in a multiple-choice
        /// interaction).
        /// </summary>
        [JsonProperty("description")]
        [ValidateProperty]
        public LanguageMap Description { get; set; }
    }
}
