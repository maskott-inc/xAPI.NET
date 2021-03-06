﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using xAPI.Client.Exceptions;
using xAPI.Client.Http;
using xAPI.Client.Http.Options;
using xAPI.Client.Json;
using xAPI.Client.Requests;
using xAPI.Client.Resources;
using xAPI.Client.Utils;

namespace xAPI.Client.Endpoints.Impl
{
    internal class StatementsApi : IStatementsApi
    {
        private const string ENDPOINT = "statements";
        private const string XAPI_CONSISTENT_THROUGH_HEADER = "X-Experience-API-Consistent-Through";
        private readonly IHttpClientWrapper _client;

        public StatementsApi(IHttpClientWrapper client)
        {
            this._client = client;
        }

        async Task<Statement> IStatementsApi.Get(GetStatementRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            try
            {
                HttpResponseMessage response = await this._client.GetJson(options);
                return await response.Content.ReadAsAsync<Statement>(new[] { new StrictJsonMediaTypeFormatter() });
            }
            catch (NotFoundException)
            {
                return null;
            }
        }

        async Task<bool> IStatementsApi.Put(PutStatementRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT) { NullValueHandling = NullValueHandling.Ignore };
            this.CompleteOptions(options, request);

            try
            {
                await this._client.PutJson(options, request.Statement);
                return true;
            }
            catch (ConflictException)
            {
                return false;
            }
        }

        async Task<Guid?> IStatementsApi.Post(PostStatementRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT) { NullValueHandling = NullValueHandling.Ignore };
            this.CompleteOptions(options, request);

            try
            {
                HttpResponseMessage response = await this._client.PostJson(options, request.Statement);
                List<Guid> content = await response.Content.ReadAsAsync<List<Guid>>(new[] { new StrictJsonMediaTypeFormatter() });
                return content?.FirstOrDefault();
            }
            catch (ConflictException)
            {
                return null;
            }
        }

        async Task<StatementResult> IStatementsApi.GetMany(GetStatementsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            HttpResponseMessage response = await this._client.GetJson(options);
            StatementResult content = await response.Content.ReadAsAsync<StatementResult>(new[] { new StrictJsonMediaTypeFormatter() });
            content.ConsistentThrough = this.GetConsistentThroughHeader(response.Headers);
            return content;
        }

        async Task<StatementResult> IStatementsApi.GetMore(Uri more)
        {
            if (more == null)
            {
                throw new ArgumentNullException(nameof(more));
            }
            if (more.IsAbsoluteUri)
            {
                throw new ArgumentException("The URI must be relative", nameof(more));
            }

            string endpoint = $"/{more.ToString().TrimStart('/')}";
            var options = new RequestOptions(endpoint);

            HttpResponseMessage response = await this._client.GetJson(options);
            StatementResult content = await response.Content.ReadAsAsync<StatementResult>(new[] { new StrictJsonMediaTypeFormatter() });
            content.ConsistentThrough = this.GetConsistentThroughHeader(response.Headers);
            return content;
        }

        async Task<List<Guid>> IStatementsApi.PostMany(PostStatementsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Validate();

            var options = new RequestOptions(ENDPOINT);
            this.CompleteOptions(options, request);

            try
            {
                HttpResponseMessage response = await this._client.PostJson(options, request.Statements);
                return await response.Content.ReadAsAsync<List<Guid>>(new[] { new StrictJsonMediaTypeFormatter() });
            }
            catch (ConflictException)
            {
                return new List<Guid>();
            }
        }

        private void CompleteOptions(RequestOptions options, GetStatementRequest request)
        {
            if (request.StatementId.HasValue)
            {
                options.QueryStringParameters.Add("statementId", request.StatementId.Value.ToString());
            }
            if (request.VoidedStatementId.HasValue)
            {
                options.QueryStringParameters.Add("voidedStatementId", request.VoidedStatementId.Value.ToString());
            }

            foreach (string language in request.GetAcceptedLanguages())
            {
                options.CustomHeaders.Add("Accept-Language", language);
            }
        }

        private void CompleteOptions(RequestOptions options, PutStatementRequest request)
        {
            options.QueryStringParameters.Add("statementId", request.Statement.Id.ToString());
        }

        private void CompleteOptions(RequestOptions options, PostStatementRequest request)
        {
        }

        private void CompleteOptions(RequestOptions options, GetStatementsRequest request)
        {
            if (request.Agent != null)
            {
                string agentStr = JsonConvert.SerializeObject(request.Agent, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
                options.QueryStringParameters.Add("agent", agentStr);
            }
            if (request.Verb != null)
            {
                options.QueryStringParameters.Add("verb", request.Verb.ToString());
            }
            if (request.ActivityId != null)
            {
                options.QueryStringParameters.Add("activity", request.ActivityId.ToString());
            }
            if (request.Registration.HasValue)
            {
                options.QueryStringParameters.Add("registration", request.Registration.Value.ToString());
            }
            if (request.RelatedActivities)
            {
                options.QueryStringParameters.Add("related_activities", bool.TrueString.ToLowerInvariant());
            }
            if (request.RelatedAgents)
            {
                options.QueryStringParameters.Add("related_agents", bool.TrueString.ToLowerInvariant());
            }
            if (request.Since.HasValue)
            {
                options.QueryStringParameters.Add("since", request.Since.Value.ToString(Constants.DATETIME_FORMAT));
            }
            if (request.Until.HasValue)
            {
                options.QueryStringParameters.Add("until", request.Since.Value.ToString(Constants.DATETIME_FORMAT));
            }
            if (request.Limit > 0)
            {
                options.QueryStringParameters.Add("limit", request.Limit.ToString());
            }
            if (request.Format != StatementFormat.Exact)
            {
                options.QueryStringParameters.Add("format", EnumHelper.ToEnumString(request.Format));
            }
            if (request.Attachments)
            {
                options.QueryStringParameters.Add("attachments", bool.TrueString.ToLowerInvariant());
            }
            if (request.Ascending)
            {
                options.QueryStringParameters.Add("ascending", bool.TrueString.ToLowerInvariant());
            }

            foreach (string language in request.GetAcceptedLanguages())
            {
                options.CustomHeaders.Add("Accept-Language", language);
            }
        }

        private void CompleteOptions(RequestOptions options, PostStatementsRequest request)
        {
        }

        private DateTimeOffset GetConsistentThroughHeader(HttpResponseHeaders headers)
        {
            if (!headers.TryGetValues(XAPI_CONSISTENT_THROUGH_HEADER, out IEnumerable<string> values))
            {
                throw new LRSException($"Header {XAPI_CONSISTENT_THROUGH_HEADER} is missing from LRS response");
            }

            string header = values.First();
            if (!DateTimeOffset.TryParse(header, out DateTimeOffset date))
            {
                throw new LRSException($"Header {XAPI_CONSISTENT_THROUGH_HEADER} is not in a valid DateTime format");
            }

            return date;
        }
    }
}
