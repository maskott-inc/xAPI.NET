using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    /// <summary>
    /// The state resource.
    /// 
    /// Generally, this is a scratch area for Learning Record Providers
    /// that do not have their own internal storage, or need to
    /// persist state across devices. This resource has Concurrency
    /// controls associated with it.
    /// 
    /// See <see cref="!:https://github.com/adlnet/xAPI-Spec/blob/master/xAPI-Communication.md#stateres">the specification</see>.
    /// </summary>
    public interface IStatesApi
    {
        /// <summary>
        /// Fetches the document specified by the given "stateId" that
        /// exists in the context of the specified Activity, Agent, and
        /// registration (if specified).
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns></returns>
        Task<StateDocument> Get(GetStateRequest request);

        /// <summary>
        /// Fetches the document specified by the given "stateId" that
        /// exists in the context of the specified Activity, Agent, and
        /// registration (if specified).
        /// </summary>
        /// <typeparam name="T">
        /// The custom type of the document to fetch. The type must support
        /// JSON serialization.
        /// </typeparam>
        /// <param name="request">The request parameters.</param>
        /// <returns></returns>
        Task<StateDocument<T>> Get<T>(GetStateRequest request);

        /// <summary>
        /// Stores or changes the document specified by the given "stateId" that
        /// exists in the context of the specified Activity, Agent, and
        /// registration (if specified).
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
        Task<bool> Put<T>(PutStateRequest<T> request);

        /// <summary>
        /// Stores or changes the document specified by the given "stateId" that
        /// exists in the context of the specified Activity, Agent, and
        /// registration (if specified).
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
        Task<bool> Post<T>(PostStateRequest<T> request);

        /// <summary>
        /// Deletes the document specified by the given "stateId" that
        /// exists in the context of the specified Activity, Agent, and
        /// registration (if specified).
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>
        /// True if the document was successfully deleted, false if the document
        /// could not be deleted (because the document has been updated since
        /// the last fetch).
        /// </returns>
        Task<bool> Delete(DeleteStateRequest request);

        /// <summary>
        /// Fetches State ids of all state data for this context (Activity +
        /// Agent [ + registration if specified]).
        /// If "since" parameter is specified, this is limited to entries
        /// that have been stored or updated since the specified timestamp
        /// (exclusive).
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>Array of State id(s).</returns>
        Task<List<string>> GetMany(GetStatesRequest request);

        /// <summary>
        /// Deletes all state data for this context (Activity + Agent [+
        /// registration if specified]).
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns></returns>
        Task DeleteMany(DeleteStatesRequest request);
    }
}
