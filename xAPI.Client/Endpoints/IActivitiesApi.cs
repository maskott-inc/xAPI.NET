using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    /// <summary>
    /// The Activities Resource provides a method to retrieve a full
    /// description of an Activity from the LRS. This resource has
    /// Concurrency controls associated with it.
    /// See <see cref="!:https://github.com/adlnet/xAPI-Spec/blob/master/xAPI-Communication.md#activitiesres">the specification</see>.
    /// </summary>
    public interface IActivitiesApi
    {
        /// <summary>
        /// Loads the complete Activity Object specified.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>
        /// The complete activity object, or null if it does not exist.
        /// The LRS may return a default activity object anyway, even
        /// if it does not exist.
        /// </returns>
        Task<Activity> Get(GetActivityRequest request);
    }
}
