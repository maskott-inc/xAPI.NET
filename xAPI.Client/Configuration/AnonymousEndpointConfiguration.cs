using xAPI.Client.Authenticators;

namespace xAPI.Client.Configuration
{
    public class AnonymousEndpointConfiguration : EndpointConfiguration
    {
        public override ILRSAuthenticator GetAuthenticator()
        {
            return new AnonymousAuthenticator();
        }
    }
}
