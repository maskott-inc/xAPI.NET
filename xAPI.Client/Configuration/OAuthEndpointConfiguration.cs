using xAPI.Client.Authenticators;

namespace xAPI.Client.Configuration
{
    public class OAuthEndpointConfiguration : EndpointConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public override ILRSAuthenticator GetAuthenticator()
        {
            return new OAuthAuthenticator(this);
        }
    }
}
