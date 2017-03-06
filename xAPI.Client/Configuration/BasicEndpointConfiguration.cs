using xAPI.Client.Authenticators;

namespace xAPI.Client.Configuration
{
    public class BasicEndpointConfiguration : EndpointConfiguration
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public override ILRSAuthenticator GetAuthenticator()
        {
            return new BasicHttpAuthenticator(this);
        }
    }
}
