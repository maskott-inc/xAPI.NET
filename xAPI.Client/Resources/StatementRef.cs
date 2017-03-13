using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class StatementRef : IStatementTarget, ISubStatementTarget
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public string ObjectType => "StatementRef";
    }
}
