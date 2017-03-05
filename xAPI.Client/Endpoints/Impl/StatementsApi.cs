using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;
using xAPI.Client.Results;

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

        Task IStatementsApi.Put(Statement statement)
        {
            throw new NotImplementedException();
        }

        Task IStatementsApi.Post(Statement statement)
        {
            throw new NotImplementedException();
        }

        Task<StatementsResult> IStatementsApi.GetMany(GetStatementsRequest request, string continuationToken)
        {
            throw new NotImplementedException();
        }

        Task IStatementsApi.PostMany(IReadOnlyList<Statement> statements)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
