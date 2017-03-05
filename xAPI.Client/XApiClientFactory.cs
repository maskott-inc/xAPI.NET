using xAPI.Client.Authenticators;
using xAPI.Client.Configuration;

namespace xAPI.Client
{
    public static class XApiClientFactory
    {
        public static IXApiClient CreateUsingAnonymousAuthenticator(AnonymousEndpointConfiguration config)
        {
            return CreateUsingCustomAuthenticator(new AnonymousAuthenticator(), config);
        }

        public static IXApiClient CreateUsingBasicHttpAuthenticator(BasicEndpointConfiguration config)
        {
            return CreateUsingCustomAuthenticator(new BasicHttpAuthenticator(), config);
        }

        public static IXApiClient CreateUsingOAuthAuthenticator(OAuthEndpointConfiguration config)
        {
            return CreateUsingCustomAuthenticator(new OAuthAuthenticator(), config);
        }

        public static IXApiClient CreateUsingCustomAuthenticator<T>(ILRSAuthenticator<T> authenticator, T config) where T : EndpointConfiguration
        {
            var client = new XApiClient();
            client.SetConfiguration(config);

            authenticator.SetConfiguration(config);
            client.SetAuthenticator(authenticator);

            return client;
        }
    }
}
