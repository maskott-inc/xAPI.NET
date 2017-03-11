using Newtonsoft.Json.Linq;

namespace xAPI.Client.Resources
{
    public class ActivityProfileDocument : ActivityProfileDocument<JToken>
    {
    }

    public class ActivityProfileDocument<T> : BaseDocument<T>
    {
    }
}
