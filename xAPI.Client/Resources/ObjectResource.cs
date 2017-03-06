using Newtonsoft.Json;

namespace xAPI.Client.Resources
{
    public abstract class ObjectResource
    {
        [JsonProperty("objectType")]
        public string ObjectType { get { return this.GetObjectType(); } }

        protected abstract string GetObjectType();
    }
}
