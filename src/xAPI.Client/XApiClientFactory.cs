using xAPI.Client.Configuration;

namespace xAPI.Client
{
    /// <summary>
    /// xAPI client factory
    /// </summary>
    public static class XApiClientFactory
    {
        /// <summary>
        /// Creates a new implementation of IXApiClient given a configuration class.
        /// </summary>
        /// <param name="config">
        /// The configuration class is used to set the base xAPI parameters (LRS endpoint, version)
        /// as well as selecting and configure the appropriate authenticator used by the client.
        /// </param>
        /// <returns>A new implementation of IXApiClient</returns>
        public static IXApiClient Create(EndpointConfiguration config)
        {
            var client = new XApiClient();
            client.SetConfiguration(config);
            return client;
        }
    }
}
