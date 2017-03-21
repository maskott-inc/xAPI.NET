using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    /// <summary>
    /// The Agent Profile Resource is much like the State Resource,
    /// allowing for arbitrary key / document pairs to be saved which
    /// are related to an Agent.
    /// See <see cref="!:https://github.com/adlnet/xAPI-Spec/blob/master/xAPI-Communication.md#agentprofres">the specification</see>.
    /// </summary>
    public interface IAgentProfilesApi
    {
        /// <summary>
        /// Fetches the specified Profile document in the context of
        /// the specified Agent.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns></returns>
        Task<AgentProfileDocument> Get(GetAgentProfileRequest request);

        /// <summary>
        /// Fetches the specified Profile document in the context of
        /// the specified Agent.
        /// </summary>
        /// <typeparam name="T">
        /// The custom type of the document to fetch. The type must support
        /// JSON serialization.
        /// </typeparam>
        /// <param name="request">The request parameters.</param>
        /// <returns></returns>
        Task<AgentProfileDocument<T>> Get<T>(GetAgentProfileRequest request);

        /// <summary>
        /// Stores or changes the specified Profile document in the context
        /// of the specified Agent.
        /// </summary>
        /// <typeparam name="T">
        /// The custom type of the document to store. The type must support
        /// JSON serialization.
        /// </typeparam>
        /// <param name="request">The request parameters.</param>
        /// <returns>
        /// True if the document was successfully added or updated, false if
        /// the document could not be added (because it already exists) or updated
        /// (because it was already updated since the last fetch).
        /// </returns>
        Task<bool> Put<T>(PutAgentProfileRequest<T> request);

        /// <summary>
        /// Stores or changes the specified Profile document in the context
        /// of the specified Agent.
        /// </summary>
        /// <typeparam name="T">
        /// The custom type of the document to store. The type must support
        /// JSON serialization.
        /// In the case of an update, the document will be merged by the LRS
        /// with the existing document, so be sure that your class will not
        /// serialize properties you don't want to update.
        /// </typeparam>
        /// <param name="request">The request parameters.</param>
        /// <returns>
        /// True if the document was successfully added or updated, false if
        /// the document could not be added (because it already exists) or updated
        /// (because it was already updated since the last fetch).
        /// </returns>
        Task<bool> Post<T>(PostAgentProfileRequest<T> request);

        /// <summary>
        /// Deletes the specified Profile document in the context
        /// of the specified Agent.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>
        /// True if the document was successfully deleted, false if the document
        /// could not be deleted (because the document has been updated since
        /// the last fetch).
        /// </returns>
        Task<bool> Delete(DeleteAgentProfileRequest request);

        /// <summary>
        /// Fetches Profile ids of all Profile documents for an Agent.
        /// If "since" parameter is specified, this is limited to entries
        /// that have been stored or updated since the specified timestamp
        /// (exclusive).
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>Array of Profile id(s).</returns>
        Task<List<string>> GetMany(GetAgentProfilesRequest request);
    }
}
