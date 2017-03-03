using Newtonsoft.Json;

namespace Maskott.xAPI.Client.Resources
{
    public abstract class ObjectResource
    {
        [JsonProperty("objectType")]
        public string ObjectType { get; set; }
    }
}
