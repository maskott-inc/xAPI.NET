using xAPI.Client.Authenticators;

namespace xAPI.Client.Configuration
{
    /// <summary>
    /// The configuration used with OAuth clients. Any client created
    /// with this configuration will use a OAuthAuthenticator.
    /// </summary>
    public class OAuthEndpointConfiguration : EndpointConfiguration
    {
        /// <summary>
        /// The OAuth's client ID.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The OAuth's client secret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Returns a new instance of OAuthAuthenticator.
        /// </summary>
        /// <returns></returns>
        public override ILRSAuthenticator GetAuthenticator()
        {
            return new OAuthAuthenticator(this);
        }
    }
}
