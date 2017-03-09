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
    public class AgentProfilesTests : BaseTest
    {
        private const string AGENT_NAME = "foo";
        private const string AGENT_MBOX = "mailto:test@example.org";
        private static readonly string PROFILE_ID = Guid.NewGuid().ToString();
        private static readonly DateTimeOffset SINCE = DateTimeOffset.UtcNow.AddDays(-1);
        private static readonly DateTimeOffset LAST_MODIFIED = DateTimeOffset.UtcNow;
        private const string ETAG = "\"123456789\"";

        [Test]
        public async Task can_get_agent_profile_with_dynamic_document()
        {
            // Arrange
            var request = new GetAgentProfileRequest()
            {
                Agent = new Agent()
                {
                    Name = AGENT_NAME,
                    MBox = new Uri(AGENT_MBOX)
                },
                ProfileId = PROFILE_ID
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("profileId", PROFILE_ID)
                .Respond(this.GetAgentProfileResponseMessage());

            // Act
            AgentProfileDocument agentProfile = await this._client.AgentProfiles.Get(request);

            // Assert
            agentProfile.Should().NotBeNull();
            agentProfile.ETag.Should().Be(ETAG);
            agentProfile.LastModified.Should().Be(LAST_MODIFIED);
            string content = agentProfile.Content;
            content.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task can_get_agent_profile_with_string_document()
        {
            // Arrange
            var request = new GetAgentProfileRequest()
            {
                Agent = new Agent()
                {
                    Name = AGENT_NAME,
                    MBox = new Uri(AGENT_MBOX)
                },
                ProfileId = PROFILE_ID
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("profileId", PROFILE_ID)
                .Respond(this.GetAgentProfileResponseMessage());

            // Act
            AgentProfileDocument<string> agentProfile = await this._client.AgentProfiles.Get<string>(request);

            // Assert
            agentProfile.Should().NotBeNull();
            agentProfile.ETag.Should().Be(ETAG);
            agentProfile.LastModified.Should().Be(LAST_MODIFIED);
            agentProfile.Content.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void cannot_get_agent_profile_when_unauthorized()
        {
            // Arrange
            var request = new GetAgentProfileRequest()
            {
                Agent = new Agent()
                {
                    Name = AGENT_NAME,
                    MBox = new Uri(AGENT_MBOX)
                },
                ProfileId = PROFILE_ID
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("profileId", PROFILE_ID)
                .Respond(HttpStatusCode.Forbidden);

            // Act
            Func<Task> action = async () =>
            {
                await this._client.AgentProfiles.Get(request);
            };

            // Assert
            action.ShouldThrow<ForbiddenException>();
        }

        [Test]
        public async Task can_put_new_agent_profile()
        {
            // Arrange
            var state = new AgentProfileDocument<string>()
            {
                Content = "foo"
            };
            var request = PutAgentProfileRequest.Create(state);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-None-Match", "*")
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.AgentProfiles.Put(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task can_put_existing_agent_profile()
        {
            // Arrange
            var state = new AgentProfileDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = PutAgentProfileRequest.Create(state);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.AgentProfiles.Put(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task cannot_put_existing_agent_profile_when_etag_is_invalid()
        {
            // Arrange
            var state = new AgentProfileDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = PutAgentProfileRequest.Create(state);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.PreconditionFailed);

            // Act
            bool result = await this._client.AgentProfiles.Put(request);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task can_post_new_agent_profile()
        {
            // Arrange
            var state = new AgentProfileDocument<string>()
            {
                Content = "foo"
            };
            var request = PostAgentProfileRequest.Create(state);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-None-Match", "*")
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.AgentProfiles.Post(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task can_post_existing_agent_profile()
        {
            // Arrange
            var state = new AgentProfileDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = PostAgentProfileRequest.Create(state);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.AgentProfiles.Post(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task cannot_post_existing_agent_profile_when_etag_is_invalid()
        {
            // Arrange
            var state = new AgentProfileDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = PostAgentProfileRequest.Create(state);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.PreconditionFailed);

            // Act
            bool result = await this._client.AgentProfiles.Post(request);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task can_delete_existing_agent_profile()
        {
            // Arrange
            var state = new AgentProfileDocument<string>()
            {
                Content = "foo"
            };
            var request = DeleteAgentProfileRequest.Create(state);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Delete, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("profileId", PROFILE_ID)
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.AgentProfiles.Delete(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task can_delete_existing_agent_profile_with_etag()
        {
            // Arrange
            var state = new AgentProfileDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = DeleteAgentProfileRequest.Create(state);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Delete, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.AgentProfiles.Delete(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task cannot_delete_existing_agent_profile_with_invalid_etag()
        {
            // Arrange
            var state = new AgentProfileDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = DeleteAgentProfileRequest.Create(state);
            request.Agent = new Agent()
            {
                Name = AGENT_NAME,
                MBox = new Uri(AGENT_MBOX)
            };
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Delete, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.PreconditionFailed);

            // Act
            bool result = await this._client.AgentProfiles.Delete(request);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task can_get_many_agent_profiles()
        {
            // Arrange
            var request = new GetAgentProfilesRequest()
            {
                Agent = new Agent()
                {
                    Name = AGENT_NAME,
                    MBox = new Uri(AGENT_MBOX)
                },
                Since = SINCE
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("agents/profile"))
                .WithQueryString("agent", $"{{\"name\":\"{AGENT_NAME}\",\"mbox\":\"{AGENT_MBOX}\",\"objectType\":\"Agent\"}}")
                .WithQueryString("since", SINCE.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"))
                .Respond(HttpStatusCode.OK, "application/json", this.ReadDataFile("agents/profile/get_many.json"));

            // Act
            List<string> stateIds = await this._client.AgentProfiles.GetMany(request);

            // Assert
            stateIds.Should().NotBeNullOrEmpty();
        }

        private HttpResponseMessage GetAgentProfileResponseMessage()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.ETag = new EntityTagHeaderValue(ETAG);
            response.Content = new StringContent(this.ReadDataFile("agents/profile/get.json"), Encoding.UTF8, "application/json");
            response.Content.Headers.LastModified = LAST_MODIFIED;

            return response;
        }
    }
}
