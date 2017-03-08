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

namespace xAPI.Client.Tests
{
    public class StatesTests : BaseTest
    {
        private const string ACTIVITY_ID = "http://www.example.org/activity";
        private const string AGENT_NAME = "foo";
        private const string AGENT_MBOX = "mailto:test@example.org";
        private static readonly Guid REGISTRATION = Guid.NewGuid();
        private static readonly string STATE_ID = "bar";
        private static readonly DateTimeOffset SINCE = DateTimeOffset.UtcNow.AddDays(-1);
        private static readonly DateTimeOffset LAST_MODIFIED = DateTimeOffset.UtcNow;
        private const string ETAG = "\"123456789\"";

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
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .Respond(this.GetStateResponseMessage());

            // Act
            StateDocument state = await this._client.States.Get(request);

            // Assert
            state.Should().NotBeNull();
            state.ETag.Should().Be(ETAG);
            state.LastModified.Should().Be(LAST_MODIFIED);
            string content = state.Content;
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
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
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
        public void cannot_get_state_if_unauthorized()
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
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
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
        public void can_put_state()
        {
            // Arrange
            var request = new PutStateRequest()
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
            var state = new StateDocument<string>()
            {
                Content = "foo"
            };
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .WithHeaders("If-None-Match", "*")
                .Respond(HttpStatusCode.NoContent);

            // Act
            Func<Task> action = async () => await this._client.States.Put(request, state);

            // Assert
            action.ShouldNotThrow();
        }

        [Test]
        public void can_post_state()
        {
            // Arrange
            var request = new PostStateRequest()
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
            var state = new StateDocument<string>()
            {
                Content = "foo"
            };
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .WithHeaders("If-None-Match", "*")
                .Respond(HttpStatusCode.NoContent);

            // Act
            Func<Task> action = async () => await this._client.States.Post(request, state);

            // Assert
            action.ShouldNotThrow();
        }

        [Test]
        public void can_delete_state()
        {
            // Arrange
            var request = new DeleteStateRequest()
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
                .When(HttpMethod.Delete, this.GetApiUrl("activities/state"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("stateId", STATE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.NoContent);

            // Act
            Func<Task> action = async () => await this._client.States.Delete(request);

            // Assert
            action.ShouldNotThrow();
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
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("registration", REGISTRATION.ToString())
                .WithQueryString("since", SINCE.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"))
                .Respond(HttpStatusCode.OK, "application/json", this.ReadDataFile("activities/state/get_many.json"));

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
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
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
            response.Content = new StringContent(this.ReadDataFile("activities/state/get.json"), Encoding.UTF8, "application/json");
            response.Content.Headers.LastModified = LAST_MODIFIED;

            return response;
        }
    }
}
