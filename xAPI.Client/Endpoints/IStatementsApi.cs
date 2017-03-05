using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;
using xAPI.Client.Results;

namespace xAPI.Client.Endpoints
{
    public interface IStatementsApi
    {
        Task Put(Statement statement);
        Task Post(Statement statement);
        Task<StatementsResult> GetMany(GetStatementsRequest request, string continuationToken = null);
        Task PostMany(IReadOnlyList<Statement> statements);
    }
}
