using Newtonsoft.Json.Linq;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// An activity profile document with a flexible structure.
    /// </summary>
    public class ActivityProfileDocument : ActivityProfileDocument<JToken>
    {
    }

    /// <summary>
    /// An activity profile document with a strongly typed structure.
    /// </summary>
    /// <typeparam name="T">The document's type. Must be JSON serializable.</typeparam>
    public class ActivityProfileDocument<T> : BaseDocument<T>
    {
    }
}
