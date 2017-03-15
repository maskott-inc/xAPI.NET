using System.Collections.Generic;

namespace xAPI.Client.Http
{
    internal class GetJsonOptions : BaseJsonOptions
    {
        public List<string> AcceptedLanguages { get; set; }
    }
}
