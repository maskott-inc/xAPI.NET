using Newtonsoft.Json;

namespace xAPI.Client.Resources
{
    public abstract class ObjectResource
    {
        [JsonProperty("objectType")]
        public string ObjectType { get; set; }
    }
}
