using xAPI.Client.Configuration;

namespace xAPI.Client
{
    public static class XApiClientFactory
    {
        public static IXApiClient Create(EndpointConfiguration config)
        {
            var client = new XApiClient();
            client.SetConfiguration(config);
            return client;
        }
    }
}
