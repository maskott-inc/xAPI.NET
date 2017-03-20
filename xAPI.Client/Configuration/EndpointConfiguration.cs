using System;
using System.Net.Http;
using xAPI.Client.Authenticators;
using xAPI.Client.Resources;

namespace xAPI.Client.Configuration
{
    /// <summary>
    /// The base configuration infos, required to setup a
    /// minimal xAPI client instance.
    /// </summary>
    public abstract class EndpointConfiguration
    {
        /// <summary>
        /// The base LRS endpoint URI. This will be used as a
        /// base adress for all requests made against the LRS.
        /// </summary>
        public Uri EndpointUri { get; set; }

        /// <summary>
        /// The xAPI version used by the client. This will be
        /// used as a HTTP header for all requests made against
        /// the LRS.
        /// </summary>
        public XApiVersion Version { get; set; }

        private HttpClient _httpClient;
        /// <summary>
        /// An instance of .NET HttpClient. If you don't provide
        /// any implementation, the default HttpClient
        /// implementation will be used.
        /// Use this property to override with your own
        /// implementation, e.g. to enable mocking or decoration.
        /// </summary>
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

        /// <summary>
        /// Returns the authenticator associated with this configuration
        /// object.
        /// </summary>
        /// <returns>An implementation of ILRSAuthenticator.</returns>
        public abstract ILRSAuthenticator GetAuthenticator();
    }
}
