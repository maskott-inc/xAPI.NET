using FluentAssertions;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using xAPI.Client.Exceptions;
using xAPI.Client.Requests;
using xAPI.Client.Resources;
using xAPI.Client.Tests.Data;

namespace xAPI.Client.Tests
{
    public class StatesTests : BaseEndpointTest
    {
        private const string ACTIVITY_ID = "http://www.example.org/activity";
        private const string AGENT_NAME = "foo";
        private const string AGENT_MBOX = "mailto:test@example.org";
        private static readonly Guid REGISTRATION = Guid.NewGuid();
        private static readonly string STATE_ID = "bar";
        private static readonly DateTimeOffset SINCE = DateTimeOffset.UtcNow.AddDays(-1);
        private static readonly DateTimeOffset LAST_MODIFIED = DateTimeOffset.UtcNow;
        private const string ETAG = "\"123456789\"";
        private static readonly string AGENT_QS = $"{{\"objectType\":\"Agent\",\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\"}}";

        [Test]
        public async Task can_get_state_with_dynamic_document()
        {
            // Arrange
            var request = new GetStateRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID),
                Agent = new Agent()
                {
                    Name = AGENT_NAME,
                    MBox = new Uri(AGENT_MBOX)
                },
                Registration = REGISTRATION,
                StateId = STATE_ID
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .Respond(this.GetStateResponseMessage());

            // Act
            StateDocument state = await this._client.States.Get(request);

            // Assert
            state.Should().NotBeNull();
            state.ETag.Should().Be(ETAG);
            state.LastModified.Should().Be(LAST_MODIFIED);
            string content = state.Content.ToObject<string>();
            content.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task can_get_state_with_string_document()
        {
            // Arrange
            var request = new GetStateRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID),
                Agent = new Agent()
                {
                    Name = AGENT_NAME,
                    MBox = new Uri(AGENT_MBOX)
                },
                Registration = REGISTRATION,
                StateId = STATE_ID
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .Respond(this.GetStateResponseMessage());

            // Act
            StateDocument<string> state = await this._client.States.Get<string>(request);

            // Assert
            state.Should().NotBeNull();
            state.ETag.Should().Be(ETAG);
            state.LastModified.Should().Be(LAST_MODIFIED);
            state.Content.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void cannot_get_state_when_unauthorized()
        {
            // Arrange
            var request = new GetStateRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID),
                Agent = new Agent()
                {
                    Name = AGENT_NAME,
                    MBox = new Uri(AGENT_MBOX)
                },
                Registration = REGISTRATION,
                StateId = STATE_ID
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .Respond(HttpStatusCode.Forbidden);

            // Act
            Func<Task> action = async () =>
            {
                await this._client.States.Get(request);
            };

            // Assert
            action.ShouldThrow<ForbiddenException>();
        }

        [Test]
        public async Task can_put_new_state()
        {
            // Arrange
            var state = new StateDocument<string>()
            {
                Content = "foo"
            };
            var request = PutStateRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.Registration = REGISTRATION;
            request.StateId = STATE_ID;
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .WithHeaders("If-None-Match", "*")
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.States.Put(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task can_put_existing_state()
        {
            // Arrange
            var state = new StateDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = PutStateRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.Registration = REGISTRATION;
            request.StateId = STATE_ID;
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.States.Put(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task cannot_put_existing_state_when_etag_is_invalid()
        {
            // Arrange
            var state = new StateDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = PutStateRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.Registration = REGISTRATION;
            request.StateId = STATE_ID;
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.PreconditionFailed);

            // Act
            bool result = await this._client.States.Put(request);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task can_post_new_state()
        {
            // Arrange
            var state = new StateDocument<string>()
            {
                Content = "foo"
            };
            var request = PostStateRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.Registration = REGISTRATION;
            request.StateId = STATE_ID;
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .WithHeaders("If-None-Match", "*")
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.States.Post(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task can_post_existing_state()
        {
            // Arrange
            var state = new StateDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = PostStateRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.Registration = REGISTRATION;
            request.StateId = STATE_ID;
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.States.Post(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task cannot_post_existing_state_when_etag_is_invalid()
        {
            // Arrange
            var state = new StateDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = PostStateRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.Registration = REGISTRATION;
            request.StateId = STATE_ID;
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.PreconditionFailed);

            // Act
            bool result = await this._client.States.Post(request);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task can_delete_existing_state()
        {
            // Arrange
            var state = new StateDocument<string>()
            {
                Content = "foo"
            };
            var request = DeleteStateRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.Registration = REGISTRATION;
            request.StateId = STATE_ID;
            this._mockHttp
                .When(HttpMethod.Delete, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .With(x => x.Headers.IfNoneMatch.Count == 0 && x.Headers.IfMatch.Count == 0)
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.States.Delete(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task can_delete_existing_state_with_etag()
        {
            // Arrange
            var state = new StateDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = DeleteStateRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.Registration = REGISTRATION;
            request.StateId = STATE_ID;
            this._mockHttp
                .When(HttpMethod.Delete, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.States.Delete(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task cannot_delete_existing_state_with_invalid_etag()
        {
            // Arrange
            var state = new StateDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = DeleteStateRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.Registration = REGISTRATION;
            request.StateId = STATE_ID;
            this._mockHttp
                .When(HttpMethod.Delete, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.PreconditionFailed);

            // Act
            bool result = await this._client.States.Delete(request);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task can_get_many_states()
        {
            // Arrange
            var request = new GetStatesRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID),
                Agent = new Agent()
                {
                    Name = AGENT_NAME,
                    MBox = new Uri(AGENT_MBOX)
                },
                Registration = REGISTRATION,
                Since = SINCE
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("since", SINCE.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"))
                .Respond(HttpStatusCode.OK, "application/json", this.ReadDataFile(Constants.ACTIVITY_STATES));

            // Act
            List<string> stateIds = await this._client.States.GetMany(request);

            // Assert
            stateIds.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void can_delete_many_states()
        {
            // Arrange
            var request = new DeleteStatesRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID),
                Agent = new Agent()
                {
                    Name = AGENT_NAME,
                    MBox = new Uri(AGENT_MBOX)
                },
                Registration = REGISTRATION
            };
            this._mockHttp
                .When(HttpMethod.Delete, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", AGENT_QS)
                .WithQueryString("registration", REGISTRATION.ToString())
                .Respond(HttpStatusCode.NoContent);

            // Act
            Func<Task> action = async () => await this._client.States.DeleteMany(request);

            // Assert
            action.ShouldNotThrow();
        }

        private HttpResponseMessage GetStateResponseMessage()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.ETag = new EntityTagHeaderValue(ETAG);
            response.Content = new StringContent(this.ReadDataFile(Constants.ACTIVITY_STATE), Encoding.UTF8, "application/json");
            response.Content.Headers.LastModified = LAST_MODIFIED;

            return response;
        }
    }
}
