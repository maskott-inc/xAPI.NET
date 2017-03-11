using Newtonsoft.Json.Linq;

namespace xAPI.Client.Resources
{
    public class StateDocument : StateDocument<JToken>
    {
    }

    public class StateDocument<T> : BaseDocument<T>
    {
    }
}
