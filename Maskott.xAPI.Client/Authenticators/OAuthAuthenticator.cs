using Maskott.xAPI.Client.Configuration;
using System;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Authenticators
{
    public class OAuthAuthenticator : ILRSAuthenticator<OAuthEndpointConfiguration>
    {
        private string _clientId;
        private string _clientSecret;

        Task<AuthorizationHeaderInfos> ILRSAuthenticator.GetAuthorization()
        {
            throw new NotImplementedException();
        }

        void ILRSAuthenticator<OAuthEndpointConfiguration>.SetConfiguration(OAuthEndpointConfiguration config)
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
    }
}
