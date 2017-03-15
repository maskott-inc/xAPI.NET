using Newtonsoft.Json;

namespace xAPI.Client.Http
{
    internal abstract class BaseJsonOptions
    {
        public NullValueHandling NullValueHandling { get; set; }
    }
}
