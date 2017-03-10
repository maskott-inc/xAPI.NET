using Newtonsoft.Json;
using xAPI.Client.Json;

namespace xAPI.Client.Resources
{
    [JsonConverter(typeof(ObjectResourceConverter))]
    public interface IObjectResource
    {
        [JsonProperty("objectType")]
        string ObjectType { get; }
    }
}
