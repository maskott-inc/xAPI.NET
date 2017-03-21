using xAPI.Client.Authenticators;

namespace xAPI.Client.Configuration
{
    /// <summary>
    /// The configuration used with anonymous clients. Any client created
    /// with this configuration will use a AnonymousAuthenticator.
    /// </summary>
    public class AnonymousEndpointConfiguration : EndpointConfiguration
    {
        /// <summary>
        /// Returns a new instance of AnonymousAuthenticator.
        /// </summary>
        /// <returns></returns>
        public override ILRSAuthenticator GetAuthenticator()
        {
            return new AnonymousAuthenticator();
        }
    }
}
