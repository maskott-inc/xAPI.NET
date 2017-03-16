using System.Collections.Generic;

namespace xAPI.Client.Http.Options
{
    internal class GetJsonOptions : BaseJsonOptions
    {
        public List<string> AcceptedLanguages { get; set; }
    }
}
