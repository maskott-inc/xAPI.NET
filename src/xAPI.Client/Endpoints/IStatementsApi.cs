using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    /// <summary>
    /// The statements resource.
    /// This is the basic communication mechanism of the Experience API.
    /// See <see cref="!:https://github.com/adlnet/xAPI-Spec/blob/master/xAPI-Communication.md#stmtres">the specification</see>.
    /// </summary>
    public interface IStatementsApi
    {
        /// <summary>
        /// This method is called to fetch a single Statement.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>
        /// A single Statement, or null if the statement does not exist.
        /// </returns>
        Task<Statement> Get(GetStatementRequest request);

        /// <summary>
        /// Stores a single Statement with the given id. POST can also be
        /// used to store single Statements.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>
        /// True if the statement was successfully added, false if a different
        /// statement with the same ID already exists.
        /// </returns>
        Task<bool> Put(PutStatementRequest request);

        /// <summary>
        /// Stores a Statement.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>
        /// The statement ID, or null a different statement with the same ID
        /// already exists.
        /// </returns>
        Task<Guid?> Post(PostStatementRequest request);

        /// <summary>
        /// This method is called to fetch multiple Statements.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>
        /// A StatementResult Object, a list of Statements in reverse chronological
        /// order based on "stored" time, subject to permissions and maximum list
        /// length. If additional results are available, an IRL to retrieve them
        /// will be included in the StatementResult Object.
        /// </returns>
        Task<StatementResult> GetMany(GetStatementsRequest request);

        /// <summary>
        /// When the number of results has been limited in a StatementResult object,
        /// this method is called to fetch the next batch of statements (pagination).
        /// </summary>
        /// <param name="more">
        /// Relative IRL that can be used to fetch more results, including the full
        /// path and optionally a query string but excluding scheme, host, and port.
        /// It usually comes from an existing StatementResult object.
        /// </param>
        /// <returns>
        /// A StatementResult Object, a list of Statements in reverse chronological
        /// order based on "stored" time, subject to permissions and maximum list
        /// length. If additional results are available, an IRL to retrieve them
        /// will be included in the StatementResult Object.
        /// </returns>
        Task<StatementResult> GetMore(Uri more);

        /// <summary>
        /// Stores a set of Statements.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>
        /// The statement IDs, or an empty array if the statements could not
        /// be stored.
        /// </returns>
        Task<List<Guid>> PostMany(PostStatementsRequest request);
    }
}
