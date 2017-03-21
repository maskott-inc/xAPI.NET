using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using xAPI.Client.Json;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// A SubStatement is like a StatementRef in that it is included as part of a containing
    /// Statement, but unlike a StatementRef, it does not represent an event that has occurred.
    /// It can be used to describe, for example, a predication of a potential future Statement
    /// or the behavior a teacher looked for when evaluating a student (without representing
    /// the student actually doing that behavior).
    /// </summary>
    public class SubStatement : IStatementTarget
    {
        /// <summary>
        /// Whom the Statement is about, as an Agent or Group Object.
        /// </summary>
        [JsonProperty("actor", Required = Required.Always)]
        [JsonConverter(typeof(ObjectResourceConverter<Agent>))]
        [Required, ValidateProperty]
        public Actor Actor { get; set; }

        /// <summary>
        /// Action taken by the Actor.
        /// </summary>
        [JsonProperty("verb", Required = Required.Always)]
        [Required, ValidateProperty]
        public Verb Verb { get; set; }

        /// <summary>
        /// Activity, Agent, or another Statement that is the Object of the Statement.
        /// </summary>
        [JsonProperty("object", Required = Required.Always)]
        [JsonConverter(typeof(ObjectResourceConverter<Activity>))]
        [Required, ValidateProperty]
        public ISubStatementTarget Object { get; set; }

        /// <summary>
        /// Result Object, further details representing a measured outcome.
        /// </summary>
        [JsonProperty("result")]
        [ValidateProperty]
        public Result Result { get; set; }

        /// <summary>
        /// Context that gives the Statement more meaning. Examples: a team the Actor
        /// is working with, altitude at which a scenario was attempted in a flight
        /// simulator.
        /// </summary>
        [JsonProperty("context")]
        [ValidateProperty]
        public Context Context { get; set; }

        /// <summary>
        /// Timestamp of when the events described within this Statement occurred.
        /// Set by the LRS if not provided.
        /// </summary>
        [JsonProperty("timestamp", Required = Required.Always)]
        public DateTimeOffset? Timestamp { get; set; }

        /// <summary>
        /// Headers for Attachments to the Statement.
        /// </summary>
        [JsonProperty("attachments")]
        [ValidateProperty]
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// Always "SubStatement".
        /// </summary>
        public string ObjectType => "SubStatement";
    }
}
