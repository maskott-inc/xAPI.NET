using Newtonsoft.Json;

namespace xAPI.Client.Resources
{
    public interface IObjectResource
    {
        [JsonProperty("objectType")]
        string ObjectType { get; }
    }
}
