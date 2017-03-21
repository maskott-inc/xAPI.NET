using Newtonsoft.Json;
using System;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// An optional property that provides a place to add contextual information to a Statement.
    /// All "context" properties are optional.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// The registration that the Statement is associated with.
        /// </summary>
        [JsonProperty("registration")]
        public Guid? Registration { get; set; }

        /// <summary>
        /// Instructor that the Statement relates to, if not included as the Actor of the Statement.
        /// </summary>
        [JsonProperty("instructor")]
        [ValidateProperty]
        public Agent Instructor { get; set; }

        /// <summary>
        /// Team that this Statement relates to, if not included as the Actor of the Statement.
        /// </summary>
        [JsonProperty("team")]
        [ValidateProperty]
        public Group Team { get; set; }

        /// <summary>
        /// A map of the types of learning activity context that this Statement is related to.
        /// </summary>
        [JsonProperty("contextActivities")]
        [ValidateProperty]
        public ContextActivities ContextActivities { get; set; }

        /// <summary>
        /// Revision of the learning activity associated with this Statement. Format is free.
        /// </summary>
        [JsonProperty("revision")]
        public string Revision { get; set; }

        /// <summary>
        /// Platform used in the experience of this learning activity.
        /// </summary>
        [JsonProperty("platform")]
        public string Platform { get; set; }

        /// <summary>
        /// Code representing the language in which the experience being recorded in this Statement
        /// (mainly) occurred in, if applicable and known.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Another Statement to be considered as context for this Statement.
        /// </summary>
        [JsonProperty("statement")]
        [ValidateProperty]
        public StatementRef Statement { get; set; }

        /// <summary>
        /// A map of any other domain-specific context relevant to this Statement. For example,
        /// in a flight simulator altitude, airspeed, wind, attitude, GPS coordinates might all
        /// be relevant.
        /// </summary>
        [JsonProperty("extensions")]
        [ValidateProperty]
        public Extensions Extensions { get; set; }
    }
}
