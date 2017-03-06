using System;
using System.Threading.Tasks;
using xAPI.Client.Configuration;

namespace xAPI.Client.Authenticators
{
    public class OAuthAuthenticator : ILRSAuthenticator
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

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
