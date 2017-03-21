using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    /// <summary>
    /// The Agents Resource provides a method to retrieve a special Object
    /// with combined information about an Agent derived from an outside
    /// service, such as a directory service. This resource has Concurrency
    /// controls associated with it.
    /// See <see cref="!:https://github.com/adlnet/xAPI-Spec/blob/master/xAPI-Communication.md#agentsres">the specification</see>.
    /// </summary>
    public interface IAgentsApi
    {
        /// <summary>
        /// Return a special, Person Object for a specified Agent. The Person
        /// Object is very similar to an Agent Object, but instead of each
        /// attribute having a single value, each attribute has an array
        /// value, and it is legal to include multiple identifying properties.
        /// This is different from the FOAF concept of person, person is
        /// being used here to indicate a person-centric view of the LRS Agent
        /// data, but Agents just refer to one persona (a person in one
        /// context).
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>
        /// The complete Person object.
        /// If the LRS does not have any additional information about an Agent
        /// to return, the LRS MUST still return a Person Object when queried,
        /// but that Person Object will only include the information associated
        /// with the requested Agent.
        /// </returns>
        Task<Person> Get(GetAgentRequest request);
    }
}
