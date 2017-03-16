using FluentAssertions;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using xAPI.Client.Exceptions;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Tests
{
    public class StatementsTests : BaseEndpointTest
    {
        private static readonly Guid STATEMENT_ID = new Guid("f5a6b27d-4f4e-4d62-812b-a6b1891bfe43");
        private static readonly Guid STATEMENT_ID_2 = new Guid("b2aa659b-7ca4-46bf-90f6-c4c9c79b88e7");
        private const string ACTIVITY_ID = "http://www.example.org/activity";
        private const string ACTIVITY_NAME = "foo";
        private const string ACTIVITY_TYPE = "http://adlnet.gov/expapi/activities/meeting";
        private const string AGENT_NAME = "foo";
        private const string AGENT_MBOX = "mailto:test@example.org";
        private const string VERB = "http://www.example.org/verb";
        private const string MORE = "more?foo=bar";
        private const string XAPI_CONSISTENT_THROUGH_HEADER = "X-Experience-API-Consistent-Through";
        private const string XAPI_CONSISTENT_THROUGH_VALUE = "2017-01-01T00:00:00Z";

        [Test]
        public async Task can_get_single_statement()
        {
            // Arrange
            var request = new GetStatementRequest()
            {
                StatementId = STATEMENT_ID
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("statements"))
                .WithQueryString("statementId", STATEMENT_ID.ToString())
                .WithHeaders("Accept-Language", "*")
                .Respond(HttpStatusCode.OK, "application/json", this.ReadDataFile("statements/get.json"));

            // Act
            Statement statement = await this._client.Statements.Get(request);

            // Assert
            statement.Should().NotBeNull();
            statement.Id.Should().Be(STATEMENT_ID);
        }

        [Test]
        public void cannot_get_single_statement_when_unauthorized()
        {
            // Arrange
            var request = new GetStatementRequest()
            {
                StatementId = STATEMENT_ID
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("statements"))
                .WithQueryString("statementId", STATEMENT_ID.ToString())
                .WithHeaders("Accept-Language", "*")
                .Respond(HttpStatusCode.Forbidden);

            // Act
            Func<Task> action = async () =>
            {
                await this._client.Statements.Get(request);
            };

            // Assert
            action.ShouldThrow<ForbiddenException>();
        }

        [Test]
        public async Task can_get_single_voided_statement()
        {
            // Arrange
            var request = new GetStatementRequest()
            {
                VoidedStatementId = STATEMENT_ID
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("statements"))
                .WithQueryString("voidedStatementId", STATEMENT_ID.ToString())
                .WithHeaders("Accept-Language", "*")
                .Respond(HttpStatusCode.OK, "application/json", this.ReadDataFile("statements/get.json"));

            // Act
            Statement statement = await this._client.Statements.Get(request);

            // Assert
            statement.Should().NotBeNull();
            statement.Id.Should().Be(STATEMENT_ID);
        }

        [Test]
        public async Task can_put_new_statement()
        {
            // Arrange
            Statement statement = this.GetStatement(STATEMENT_ID);
            var request = new PutStatementRequest(statement);
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("statements"))
                .WithQueryString("statementId", STATEMENT_ID.ToString())
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.Statements.Put(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task cannot_put_existing_statement()
        {
            // Arrange
            Statement statement = this.GetStatement(STATEMENT_ID);
            var request = new PutStatementRequest(statement);
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("statements"))
                .WithQueryString("statementId", STATEMENT_ID.ToString())
                .Respond(HttpStatusCode.Conflict);

            // Act
            bool result = await this._client.Statements.Put(request);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void cannot_put_new_statement_when_unauthorized()
        {
            // Arrange
            Statement statement = this.GetStatement(STATEMENT_ID);
            var request = new PutStatementRequest(statement);
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("statements"))
                .WithQueryString("statementId", STATEMENT_ID.ToString())
                .Respond(HttpStatusCode.Forbidden);

            // Act
            Func<Task> action = async () =>
            {
                await this._client.Statements.Put(request);
            };

            // Assert
            action.ShouldThrow<ForbiddenException>();
        }

        [Test]
        public async Task can_put_voiding_statement()
        {
            // Arrange
            Statement statement = this.GetVoidingStatement(STATEMENT_ID, STATEMENT_ID_2);
            var request = new PutStatementRequest(statement);
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("statements"))
                .WithQueryString("statementId", STATEMENT_ID.ToString())
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.Statements.Put(request);

            // Assert
            result.Should().BeTrue();
        }
        [Test]
        public async Task can_post_new_statement()
        {
            // Arrange
            Statement statement = this.GetStatement(STATEMENT_ID);
            var request = new PostStatementRequest(statement);
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("statements"))
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.Statements.Post(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task cannot_post_existing_statement()
        {
            // Arrange
            Statement statement = this.GetStatement(STATEMENT_ID);
            var request = new PostStatementRequest(statement);
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("statements"))
                .Respond(HttpStatusCode.Conflict);

            // Act
            bool result = await this._client.Statements.Post(request);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void cannot_post_new_statement_when_unauthorized()
        {
            // Arrange
            Statement statement = this.GetStatement(STATEMENT_ID);
            var request = new PostStatementRequest(statement);
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("statements"))
                .Respond(HttpStatusCode.Forbidden);

            // Act
            Func<Task> action = async () =>
            {
                await this._client.Statements.Post(request);
            };

            // Assert
            action.ShouldThrow<ForbiddenException>();
        }

        [Test]
        public async Task can_post_voiding_statement()
        {
            // Arrange
            Statement statement = this.GetVoidingStatement(STATEMENT_ID, STATEMENT_ID_2);
            var request = new PostStatementRequest(statement);
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("statements"))
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.Statements.Post(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task can_get_many_statements()
        {
            // Arrange
            var request = new GetStatementsRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID)
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("statements"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithHeaders("Accept-Language", "*")
                .Respond(this.GetStatementsResponseMessage());

            // Act
            StatementResult result = await this._client.Statements.GetMany(request);

            // Assert
            result.Should().NotBeNull();
            result.Statements.Should().NotBeNullOrEmpty();
            result.More.Should().Be(new Uri(MORE));
            result.ConsistentThrough.Should().Be(DateTimeOffset.Parse(XAPI_CONSISTENT_THROUGH_VALUE));
        }

        [Test]
        public async Task can_get_more_statements()
        {
            // Arrange
            var more = new Uri(MORE, UriKind.Relative);
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl(MORE))
                .Respond(this.GetStatementsResponseMessage());

            // Act
            StatementResult result = await this._client.Statements.GetMore(more);

            // Assert
            result.Should().NotBeNull();
            result.Statements.Should().NotBeNullOrEmpty();
            result.ConsistentThrough.Should().Be(DateTimeOffset.Parse(XAPI_CONSISTENT_THROUGH_VALUE));
        }

        [Test]
        public async Task can_post_many_new_statements()
        {
            // Arrange
            List<Statement> statements = this.GetStatements();
            var request = new PostStatementsRequest(statements);
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("statements"))
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.Statements.PostMany(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task cannot_post_many_new_statements_when_one_already_exists()
        {
            // Arrange
            List<Statement> statements = this.GetStatements();
            var request = new PostStatementsRequest(statements);
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("statements"))
                .Respond(HttpStatusCode.Conflict);

            // Act
            bool result = await this._client.Statements.PostMany(request);

            // Assert
            result.Should().BeFalse();
        }

        private Statement GetStatement(Guid id)
        {
            return new Statement()
            {
                Id = id,
                Actor = new Agent()
                {
                    Name = AGENT_NAME,
                    MBox = new Uri(AGENT_MBOX)
                },
                Verb = new Verb()
                {
                    Id = new Uri(VERB)
                },
                Object = new Activity()
                {
                    Id = new Uri(ACTIVITY_ID),
                    Definition = new ActivityDefinition()
                    {
                        Name = new LanguageMap() { { "en-US", ACTIVITY_NAME } },
                        Type = new Uri(ACTIVITY_TYPE)
                    }
                }
            };
        }

        private Statement GetVoidingStatement(Guid id, Guid voidedStatementId)
        {
            var agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            return Statement.CreateVoidingStatement(id, agent, voidedStatementId);
        }

        private List<Statement> GetStatements()
        {
            return new List<Statement>()
            {
                this.GetStatement(STATEMENT_ID),
                this.GetStatement(STATEMENT_ID_2)
            };
        }

        private HttpResponseMessage GetStatementsResponseMessage()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add(XAPI_CONSISTENT_THROUGH_HEADER, XAPI_CONSISTENT_THROUGH_VALUE);
            response.Content = new StringContent(this.ReadDataFile("statements/get_many.json"), Encoding.UTF8, "application/json");

            return response;
        }
    }
}
