using Newtonsoft.Json.Linq;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// An agent profile document with a flexible structure.
    /// </summary>
    public class AgentProfileDocument : AgentProfileDocument<JToken>
    {
    }

    /// <summary>
    /// An agent profile document with a strongly typed structure.
    /// </summary>
    /// <typeparam name="T">The document's type. Must be JSON serializable.</typeparam>
    public class AgentProfileDocument<T> : BaseDocument<T>
    {
    }
}
