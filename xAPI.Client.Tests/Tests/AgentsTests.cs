using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using xAPI.Client.Configuration;
using xAPI.Client.Exceptions;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Tests.Tests
{
    public class AgentsTests : BaseTest
    {
        private IXApiClient _client;

        [SetUp]
        public void SetUpAbout()
        {
            var config = new BasicEndpointConfiguration()
            {
                EndpointUri = Config.EndpointUri,
                Version = Config.Version,
                Username = Config.BasicUsername,
                Password = Config.BasicPassword
            };
            this._client = XApiClientFactory.CreateUsingBasicHttpAuthenticator(config);
        }

        [TearDown]
        public void TearDownAbout()
        {
            this._client.Dispose();
        }

        [Test]
        public async Task can_get_activity_definition()
        {
            GetAgentRequest request = this.GetAgentRequest();
            Person actor = await this._client.Agents.Get(request);
            actor.Should().NotBeNull();
            actor.Name.Should().NotBeNull().And.HaveCount(x => x > 0).And.Contain(request.Agent.Name);
            actor.MBox.Should().NotBeNull().And.HaveCount(x => x > 0).And.Contain(request.Agent.MBox);
        }

        [Test]
        public void cannot_get_activity_definition_if_unauthorized()
        {
            var config = new BasicEndpointConfiguration()
            {
                EndpointUri = Config.EndpointUri,
                Version = Config.Version,
                Username = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            using (IXApiClient client = XApiClientFactory.CreateUsingBasicHttpAuthenticator(config))
            {
                GetAgentRequest request = this.GetAgentRequest();
                Action action = async () => await this._client.Agents.Get(request);
                action.ShouldThrow<ForbiddenException>();
            }
        }

        private GetAgentRequest GetAgentRequest()
        {
            return new GetAgentRequest()
            {
                Agent = new Agent()
                {
                    Name = "foo",
                    MBox = new Uri("mailto:test@example.org")
                }
            };
        }
    }
}
