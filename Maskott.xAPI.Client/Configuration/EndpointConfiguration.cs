using Maskott.xAPI.Client.Resources;
using System;
using System.Net.Http;

namespace Maskott.xAPI.Client.Configuration
{
    public abstract class EndpointConfiguration
    {
        public Uri EndpointUri { get; set; }
        public XApiVersion Version { get; set; }

        private HttpClient _httpClient;
        public HttpClient HttpClient
        {
            get
            {
                if (this._httpClient == null)
                {
                    this._httpClient = new HttpClient();
                }
                return this._httpClient;
            }
            set
            {
                this._httpClient = value;
            }
        }
    }
}
