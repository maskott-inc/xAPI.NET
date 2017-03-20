using Newtonsoft.Json.Linq;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// An activity state document with a flexible structure.
    /// </summary>
    public class StateDocument : StateDocument<JToken>
    {
    }

    /// <summary>
    /// An activity state document with a strongly typed structure.
    /// </summary>
    /// <typeparam name="T">The document's type. Must be JSON serializable.</typeparam>
    public class StateDocument<T> : BaseDocument<T>
    {
    }
}
