using System.Threading.Tasks;

namespace xAPI.Client.Authenticators
{
    /// <summary>
    /// A class implementing this interface can be used to provide
    /// authentication infos for the xAPI client.
    /// </summary>
    public interface ILRSAuthenticator
    {
        /// <summary>
        /// Provide the HTTP Authorization header that will be used
        /// to authenticate the client to the LRS.
        /// </summary>
        /// <returns></returns>
        Task<AuthorizationHeaderInfos> GetAuthorization();
    }
}
