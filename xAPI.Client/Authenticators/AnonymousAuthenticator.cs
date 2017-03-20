using System.Threading.Tasks;

namespace xAPI.Client.Authenticators
{
    /// <summary>
    /// This authenticator does not provide any authentication infos
    /// to the LRS (all requests are anonymous). This should not be
    /// used in production code.
    /// </summary>
    public class AnonymousAuthenticator : ILRSAuthenticator
    {
        Task<AuthorizationHeaderInfos> ILRSAuthenticator.GetAuthorization()
        {
            return Task.FromResult<AuthorizationHeaderInfos>(null);
        }
    }
}
