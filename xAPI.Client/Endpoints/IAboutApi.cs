using System.Threading.Tasks;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    /// <summary>
    /// The about resource.
    /// Primarily this resource exists to allow Clients that support
    /// multiple xAPI versions to decide which version to use when
    /// communicating with the LRS. Extensions are included to allow
    /// other uses to emerge.
    /// See <see cref="!:https://github.com/adlnet/xAPI-Spec/blob/master/xAPI-Communication.md#aboutresource">the specification</see>.
    /// </summary>
    public interface IAboutApi
    {
        /// <summary>
        /// Retrieve information about this LRS, including the xAPI
        /// version supported.
        /// </summary>
        /// <returns>Basic metadata about this LRS.</returns>
        Task<About> Get();
    }
}
