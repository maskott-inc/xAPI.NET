using System.Threading.Tasks;

namespace xAPI.Client.Authenticators
{
    public interface ILRSAuthenticator
    {
        Task<AuthorizationHeaderInfos> GetAuthorization();
    }
}
