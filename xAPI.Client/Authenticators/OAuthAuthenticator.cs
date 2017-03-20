using System;
using System.Threading.Tasks;
using xAPI.Client.Configuration;

namespace xAPI.Client.Authenticators
{
    /// <summary>
    /// This authenticator provides authentication through OAuth's Bearer
    /// scheme. It maintains a local access token cache, and renews the token
    /// only when it is expired.
    /// </summary>
    public class OAuthAuthenticator : ILRSAuthenticator
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        /// <summary>
        /// Creates a new instance of BasicHttpAuthenticator using the
        /// given configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public OAuthAuthenticator(OAuthEndpointConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            if (string.IsNullOrEmpty(config.ClientId))
            {
                throw new ArgumentNullException(nameof(config.ClientId));
            }
            if (string.IsNullOrEmpty(config.ClientSecret))
            {
                throw new ArgumentNullException(nameof(config.ClientSecret));
            }

            this._clientId = config.ClientId;
            this._clientSecret = config.ClientSecret;
        }

        Task<AuthorizationHeaderInfos> ILRSAuthenticator.GetAuthorization()
        {
            throw new NotImplementedException();
        }
    }
}
