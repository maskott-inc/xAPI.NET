using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// Is the action being done by the Actor within the Activity within
    /// a Statement. A Verb represents the "did" in "I did this".
    /// </summary>
    public class Verb
    {
        /// <summary>
        /// Corresponds to a Verb definition. Each Verb definition corresponds
        /// to the meaning of a Verb, not the word.
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public Uri Id { get; set; }

        /// <summary>
        /// The human readable representation of the Verb in one or more languages.
        /// This does not have any impact on the meaning of the Statement, but
        /// serves to give a human-readable display of the meaning already
        /// determined by the chosen Verb.
        /// </summary>
        [JsonProperty("display")]
        public LanguageMap Display { get; set; }
    }
}
