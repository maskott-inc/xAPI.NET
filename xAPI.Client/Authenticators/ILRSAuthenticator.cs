using System.Threading.Tasks;
using xAPI.Client.Configuration;

namespace xAPI.Client.Authenticators
{
    public interface ILRSAuthenticator<T> : ILRSAuthenticator where T : EndpointConfiguration
    {
        void SetConfiguration(T config);
    }

    public interface ILRSAuthenticator
    {
        Task<AuthorizationHeaderInfos> GetAuthorization();
    }
}
