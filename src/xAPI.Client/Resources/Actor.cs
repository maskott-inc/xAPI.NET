using Newtonsoft.Json;
using System;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// An individual or group representation tracked using Statements performing
    /// an action within an Activity. Is the "I" in "I did this".
    /// </summary>
    public abstract class Actor : IStatementTarget, ISubStatementTarget
    {
        /// <summary>
        /// Full name of the Agent.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The required format is "mailto:email address".
        /// Only email addresses that have only ever been and will ever be
        /// assigned to this Agent, but no others, SHOULD be used for this
        /// property and mbox_sha1sum.
        /// </summary>
        [JsonProperty("mbox")]
        public Uri MBox { get; set; }

        /// <summary>
        /// The hex-encoded SHA1 hash of a mailto IRI (i.e. the value of an
        /// mbox property). An LRS MAY include Agents with a matching hash
        /// when a request is based on an mbox.
        /// </summary>
        [JsonProperty("mbox_sha1sum")]
        public string MBoxSHA1Sum { get; set; }

        /// <summary>
        /// An openID that uniquely identifies the Agent.
        /// </summary>
        [JsonProperty("openid")]
        public Uri OpenId { get; set; }

        /// <summary>
        /// A user account on an existing system e.g. an LMS or intranet.
        /// </summary>
        [JsonProperty("account")]
        [ValidateProperty]
        public AccountObject Account { get; set; }

        /// <summary>
        /// Either "Agent" or "Group".
        /// </summary>
        public abstract string ObjectType { get; }
    }
}
