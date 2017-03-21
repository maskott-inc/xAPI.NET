using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using xAPI.Client.Json;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// A data sctructure showing evidence for any sort of experience or event which
    /// is to be tracked in xAPI as a Learning Record. A set of several Statements,
    /// each representing an event in time, might be used to track complete details
    /// about a learning experience.
    /// </summary>
    public class Statement
    {
        /// <summary>
        /// UUID assigned by LRS if not set by the Learning Record Provider.
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id { get; set; }

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
        public IStatementTarget Object { get; set; }

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
        /// Timestamp of when this Statement was recorded. Set by LRS.
        /// </summary>
        [JsonProperty("stored", Required = Required.Always)]
        public DateTimeOffset Stored { get; set; }

        /// <summary>
        /// Agent or Group who is asserting this Statement is true. Verified by the LRS
        /// based on authentication. Set by LRS if not provided or if a strong trust
        /// relationship between the Learning Record Provider and LRS has not been
        /// established.
        /// </summary>
        [JsonProperty("authority", Required = Required.Always)]
        [JsonConverter(typeof(ObjectResourceConverter<Agent>))]
        [ValidateProperty]
        public Actor Authority { get; set; }

        /// <summary>
        /// The Statement’s associated xAPI version, formatted according to Semantic
        /// Versioning 1.0.0.
        /// </summary>
        [JsonProperty("version", Required = Required.Always)]
        [ValidateProperty]
        public XApiVersion Version { get; set; }

        /// <summary>
        /// Headers for Attachments to the Statement.
        /// </summary>
        [JsonProperty("attachments")]
        [ValidateProperty]
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// Creates a new instance of the Statement class, and generates a default random
        /// ID for it.
        /// </summary>
        public Statement()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Creates a new voiding statement with a given id, agent and statement to void.
        /// </summary>
        /// <param name="id">The voiding statement's ID.</param>
        /// <param name="agent">The voiding statement's actor.</param>
        /// <param name="voidedStatementId">The ID of the statement to be voided.</param>
        /// <returns></returns>
        public static Statement CreateVoidingStatement(Guid id, Agent agent, Guid voidedStatementId)
        {
            return new Statement()
            {
                Id = id,
                Actor = agent,
                Verb = new Verb()
                {
                    Id = new Uri("http://adlnet.gov/expapi/verbs/voided"),
                    Display = new LanguageMap() { { "en-US", "voided" } }
                },
                Object = new StatementRef()
                {
                    Id = voidedStatementId
                }
            };
        }
    }
}
