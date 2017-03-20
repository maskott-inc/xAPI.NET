using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    public interface IStatementsApi
    {
        Task<Statement> Get(GetStatementRequest request);

        Task<bool> Put(PutStatementRequest request);

        Task<Guid?> Post(PostStatementRequest request);

        Task<StatementResult> GetMany(GetStatementsRequest request);

        Task<StatementResult> GetMore(Uri more);

        Task<List<Guid>> PostMany(PostStatementsRequest request);
    }
}
