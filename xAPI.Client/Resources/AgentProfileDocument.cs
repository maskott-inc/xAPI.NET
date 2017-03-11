using Newtonsoft.Json.Linq;

namespace xAPI.Client.Resources
{
    public class AgentProfileDocument : AgentProfileDocument<JToken>
    {
    }

    public class AgentProfileDocument<T> : BaseDocument<T>
    {
    }
}
