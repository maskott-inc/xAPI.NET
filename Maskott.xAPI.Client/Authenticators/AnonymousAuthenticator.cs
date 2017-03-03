using Maskott.xAPI.Client.Configuration;
using System;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Authenticators
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
