using System;
using System.Threading.Tasks;
using xAPI.Client.Configuration;

namespace xAPI.Client.Authenticators
{
    public class AnonymousAuthenticator : ILRSAuthenticator<AnonymousEndpointConfiguration>
    {
        Task<AuthorizationHeaderInfos> ILRSAuthenticator.GetAuthorization()
        {
            return Task.FromResult<AuthorizationHeaderInfos>(null);
        }

        void ILRSAuthenticator<AnonymousEndpointConfiguration>.SetConfiguration(AnonymousEndpointConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            // Nothing to store
        }
    }
}
