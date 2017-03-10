using System;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints.Impl
{
    internal class StatementsApi : IStatementsApi
    {
        private const string ENDPOINT = "statements";
        private readonly IHttpClientWrapper _client;

        public StatementsApi(IHttpClientWrapper client)
        {
            this._client = client;
        }

        #region IStatementsApi members

        Task<Statement> IStatementsApi.Get(GetStatementRequest request)
        {
            throw new NotImplementedException();
        }

        Task<bool> IStatementsApi.Put(PutStatementRequest request)
        {
            throw new NotImplementedException();
        }

        Task<bool> IStatementsApi.Post(PostStatementRequest request)
        {
            throw new NotImplementedException();
        }

        Task<StatementResult> IStatementsApi.GetMany(GetStatementsRequest request)
        {
            throw new NotImplementedException();
        }

        Task<StatementResult> IStatementsApi.GetMore(Uri more)
        {
            throw new NotImplementedException();
        }

        Task<bool> IStatementsApi.PostMany(PostStatementsRequest request)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
