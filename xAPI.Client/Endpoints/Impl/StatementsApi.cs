using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using xAPI.Client.Exceptions;
using xAPI.Client.Http;
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

        async Task<Statement> IStatementsApi.Get(GetStatementRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            string url = this.BuildUrl(request);
            return await this._client.GetJson<Statement>(url, new GetJsonOptions() { AcceptedLanguages = request.GetAcceptedLanguages() });
        }

        async Task<bool> IStatementsApi.Put(PutStatementRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            string url = this.BuildUrl(request);

            try
            {
                await this._client.PutJson(url, new PutJsonOptions() { NullValueHandling = NullValueHandling.Ignore }, request.Statement);
                return true;
            }
            catch (ConflictException)
            {
                return false;
            }
        }

        async Task<bool> IStatementsApi.Post(PostStatementRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            string url = this.BuildUrl(request);

            try
            {
                await this._client.PostJson(url, new PostJsonOptions() { NullValueHandling = NullValueHandling.Ignore }, request.Statement);
                return true;
            }
            catch (ConflictException)
            {
                return false;
            }
        }

        async Task<StatementResult> IStatementsApi.GetMany(GetStatementsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            throw new NotImplementedException();
        }

        async Task<StatementResult> IStatementsApi.GetMore(Uri more)
        {
            if (more == null)
            {
                throw new ArgumentNullException(nameof(more));
            }

            throw new NotImplementedException();
        }

        async Task<bool> IStatementsApi.PostMany(PostStatementsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            throw new NotImplementedException();
        }

        #endregion

        #region Utils

        private string BuildUrl(GetStatementRequest request)
        {
            var builder = new StringBuilder(ENDPOINT);
            if (request.StatementId.HasValue)
            {
                builder.AppendFormat("?statementId={0}", Uri.EscapeDataString(request.StatementId.Value.ToString()));
            }
            else
            {
                builder.AppendFormat("?voidedStatementId={0}", Uri.EscapeDataString(request.VoidedStatementId.Value.ToString()));
            }
            return builder.ToString();
        }

        private string BuildUrl(PutStatementRequest request)
        {
            var builder = new StringBuilder(ENDPOINT);
            builder.AppendFormat("?statementId={0}", Uri.EscapeDataString(request.Statement.Id.ToString()));
            return builder.ToString();
        }

        private string BuildUrl(PostStatementRequest request)
        {
            return ENDPOINT;
        }

        #endregion
    }
}
