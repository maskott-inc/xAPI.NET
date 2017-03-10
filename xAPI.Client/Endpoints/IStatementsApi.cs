using System;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    public interface IStatementsApi
    {
        Task<Statement> Get(GetStatementRequest request);

        Task<bool> Put(PutStatementRequest request);

        Task<bool> Post(PostStatementRequest request);

        Task<StatementResult> GetMany(GetStatementsRequest request);

        Task<StatementResult> GetMore(Uri more);

        Task<bool> PostMany(PostStatementsRequest request);
    }
}
