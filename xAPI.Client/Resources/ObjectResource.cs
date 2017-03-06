using Newtonsoft.Json;
using xAPI.Client.Json;

namespace xAPI.Client.Resources
{
    [JsonConverter(typeof(ObjectResourceConverter))]
    public abstract class ObjectResource
    {
        [JsonProperty("objectType")]
        public string ObjectType { get { return this.GetObjectType(); } }

        protected abstract string GetObjectType();
    }
}
