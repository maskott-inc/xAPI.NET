using Newtonsoft.Json;

namespace xAPI.Client.Http.Options
{
    internal abstract class BaseJsonOptions
    {
        public NullValueHandling NullValueHandling { get; set; }
    }
}
