using Maskott.xAPI.Client.Common;
using System;
using System.Net.Http;

namespace Maskott.xAPI.Client.Configuration
{
    public abstract class EndpointConfiguration
    {
        public Uri EndpointUri { get; set; }
        public XApiVersion Version { get; set; }
        public HttpMessageHandler Handler { get; set; }
    }
}
