using Newtonsoft.Json;

namespace xAPI.Client.Http
{
    internal abstract class BaseJsonOptions
    {
        public DefaultValueHandling DefaultValueHandling { get; set; }
    }
}
