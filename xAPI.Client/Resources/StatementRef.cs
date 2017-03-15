using Newtonsoft.Json;
using System;

namespace xAPI.Client.Resources
{
    public class StatementRef : IStatementTarget, ISubStatementTarget
    {
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id { get; set; }

        public string ObjectType => "StatementRef";
    }
}
