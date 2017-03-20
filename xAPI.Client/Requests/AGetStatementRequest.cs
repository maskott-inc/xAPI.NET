using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// The base class for GET statement(s) requests.
    /// </summary>
    public abstract class AGetStatementRequest : ARequest
    {
        /// <summary>
        /// The format of the statement's metadata to be returned by the request.
        /// </summary>
        public StatementFormat Format { get; set; }

        /// <summary>
        /// If true, the LRS uses the multipart response format and includes all
        /// attachments as described previously. If false, the LRS sends the prescribed
        /// response with Content-Type application/json and does not send attachment data.
        /// </summary>
        public bool Attachments { get; set; }

        /// <summary>
        /// Used to specify a preferred language.
        /// See <see cref="!:https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html">RFC 2616</see> for more details.
        /// </summary>
        public List<string> AcceptedLanguages { private get; set; }

        internal List<string> GetAcceptedLanguages()
        {
            if (this.AcceptedLanguages == null || this.AcceptedLanguages.Count == 0)
            {
                return new List<string>() { "*" };
            }
            else
            {
                return this.AcceptedLanguages;
            }
        }

        internal override void Validate()
        {
            if (this.Attachments)
            {
                throw new NotImplementedException("Attachments are not supported in this version");
            }
        }
    }

    /// <summary>
    /// Available formats for statement requests.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatementFormat
    {
        /// <summary>
        /// If ids, only include minimum information necessary in Agent, Activity, Verb
        /// and Group Objects to identify them. For Anonymous Groups this means including
        /// the minimum information needed to identify each member.
        /// </summary>
        [EnumMember(Value = "exact")]
        Exact,

        /// <summary>
        ///  If exact, return Agent, Activity, Verb and Group Objects populated exactly
        ///  as they were when the Statement was received. An LRS requesting Statements
        ///  for the purpose of importing them would use a format of "exact" in order to
        ///  maintain Statement Immutability.
        /// </summary>
        [EnumMember(Value = "ids")]
        Ids,

        /// <summary>
        ///  If canonical, return Activity Objects and Verbs populated with the canonical
        ///  definition of the Activity Objects and Display of the Verbs as determined by
        ///  the LRS, after applying the language filtering process defined below, and
        ///  return the original Agent and Group Objects as in "exact" mode.
        /// </summary>
        [EnumMember(Value = "canonical")]
        Canonical
    }
}
