using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace xAPI.Client.Requests
{
    public abstract class AGetStatementRequest : ARequest
    {
        public StatementFormat Format { get; set; }
        public bool Attachments { get; set; }

        internal override void Validate()
        {
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
