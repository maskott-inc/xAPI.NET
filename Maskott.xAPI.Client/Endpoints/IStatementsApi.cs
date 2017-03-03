using Maskott.xAPI.Client.Requests;
using Maskott.xAPI.Client.Resources;
using Maskott.xAPI.Client.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Endpoints
{
    public interface IStatementsApi
    {
        Task Put(Statement statement);
        Task Post(Statement statement);
        Task<StatementsResult> GetMany(GetStatementsRequest request, string continuationToken = null);
        Task PostMany(IReadOnlyList<Statement> statements);
    }
}
