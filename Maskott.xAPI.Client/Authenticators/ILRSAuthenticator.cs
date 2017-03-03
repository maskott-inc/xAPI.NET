using Maskott.xAPI.Client.Configuration;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Authenticators
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
