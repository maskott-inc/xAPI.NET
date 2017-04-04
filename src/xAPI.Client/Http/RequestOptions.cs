using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xAPI.Client.Json;

namespace xAPI.Client.Http.Options
{
    internal class RequestOptions
    {
        public string Path { get; private set; }

        private readonly Dictionary<string, string> _queryStringParameters = new Dictionary<string, string>();
        public Dictionary<string, string> QueryStringParameters { get { return this._queryStringParameters; } }

        private readonly Dictionary<string, string> _customHeaders = new Dictionary<string, string>();
        public Dictionary<string, string> CustomHeaders { get { return this._customHeaders; } }

        public string PathAndQuery
        {
            get
            {
                var sb = new StringBuilder(this.Path);
                if (this._queryStringParameters.Count > 0)
                {
                    string querystring = string.Join(
                        "&",
                        this._queryStringParameters.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}")
                    );
                    sb.Append("?").Append(querystring);
                }
                return sb.ToString();
            }
        }

        public NullValueHandling NullValueHandling { get; set; }

        public RequestOptions(string path)
        {
            this.Path = path;
        }

        public StrictJsonMediaTypeFormatter GetFormatter()
        {
            var formatter = new StrictJsonMediaTypeFormatter();
            formatter.SerializerSettings.NullValueHandling = this.NullValueHandling;
            return formatter;
        }
    }
}
