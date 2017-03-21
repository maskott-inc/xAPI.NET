using xAPI.Client.Authenticators;

namespace xAPI.Client.Configuration
{
    /// <summary>
    /// The configuration used with basic HTTP clients. Any client created
    /// with this configuration will use a BasicHttpAuthenticator.
    /// </summary>
    public class BasicEndpointConfiguration : EndpointConfiguration
    {
        /// <summary>
        /// The basic HTTP username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The basic HTTP password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Returns a new instance of BasicHttpAuthenticator.
        /// </summary>
        /// <returns></returns>
        public override ILRSAuthenticator GetAuthenticator()
        {
            return new BasicHttpAuthenticator(this);
        }
    }
}
