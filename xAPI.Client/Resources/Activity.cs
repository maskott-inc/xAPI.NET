using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// A type of Object making up the "this" in "I did this"; it is something with
    /// which an Actor interacted. It can be a unit of instruction, experience, or
    /// performance that is to be tracked in meaningful combination with a Verb.
    /// Interpretation of Activity is broad, meaning that Activities can even be
    /// tangible objects such as a chair (real or virtual). In the Statement "Anna
    /// tried a cake recipe", the recipe constitutes the Activity in terms of the
    /// xAPI Statement. Other examples of Activities include a book, an e-learning
    /// course, a hike, or a meeting.
    /// </summary>
    public class Activity : IStatementTarget, ISubStatementTarget
    {
        /// <summary>
        /// An identifier for a single unique Activity.
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public Uri Id { get; set; }

        /// <summary>
        /// Activity metadata.
        /// </summary>
        [JsonProperty("definition")]
        [Required, ValidateProperty]
        public ActivityDefinition Definition { get; set; }

        /// <summary>
        /// Always "Activity".
        /// </summary>
        public string ObjectType => "Activity";
    }
}
