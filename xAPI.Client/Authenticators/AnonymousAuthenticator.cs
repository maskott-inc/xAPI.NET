using System.Threading.Tasks;

namespace xAPI.Client.Authenticators
{
    public class AnonymousAuthenticator : ILRSAuthenticator
    {
        Task<AuthorizationHeaderInfos> ILRSAuthenticator.GetAuthorization()
        {
            return Task.FromResult<AuthorizationHeaderInfos>(null);
        }
    }
}
