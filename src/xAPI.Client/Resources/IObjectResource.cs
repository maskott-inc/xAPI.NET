using Newtonsoft.Json;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// An object resource is any xAPI object with an object type.
    /// </summary>
    public interface IObjectResource
    {
        /// <summary>
        /// The object type defines
        /// </summary>
        [JsonProperty("objectType")]
        string ObjectType { get; }
    }
}
