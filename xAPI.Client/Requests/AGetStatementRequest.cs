using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace xAPI.Client.Requests
{
    public abstract class AGetStatementRequest : ARequest
    {
        public StatementFormat Format { get; set; }

        public bool Attachments { get; set; }

        public List<string> AcceptedLanguages { private get; set; }

        public List<string> GetAcceptedLanguages()
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

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatementFormat
    {
        [EnumMember(Value = "exact")]
        Exact,
        [EnumMember(Value = "ids")]
        Ids,
        [EnumMember(Value = "canonical")]
        Canonical
    }
}
