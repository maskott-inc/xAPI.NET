using FluentAssertions;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using xAPI.Client.Exceptions;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Tests.Tests
{
    public class AgentsTests : BaseTest
    {
        [Test]
        public async Task can_get_agent_definition()
        {
            // Arrange
            GetAgentRequest request = new GetAgentRequest()
            {
                Agent = new Agent()
                {
                    Name = "foo",
                    MBox = new Uri("mailto:test@example.org")
                }
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("agents"))
                .Respond(HttpStatusCode.OK, "application/json", this.ReadDataFile("agents/get.json"));

            // Act
            Person actor = await this._client.Agents.Get(request);

            // Assert
            actor.Should().NotBeNull();
            actor.Name.Should().NotBeNull().And.HaveCount(x => x > 0).And.Contain(request.Agent.Name);
            actor.MBox.Should().NotBeNull().And.HaveCount(x => x > 0).And.Contain(request.Agent.MBox);
        }

        [Test]
        public void cannot_get_agent_definition_if_unauthorized()
        {
            // Arrange
            GetAgentRequest request = new GetAgentRequest()
            {
                Agent = new Agent()
                {
                    Name = "foo",
                    MBox = new Uri("mailto:test@example.org")
                }
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("agents"))
                .Respond(HttpStatusCode.Forbidden);

            // Act
            Func<Task> action = async () =>
            {
                await this._client.Agents.Get(request);
            };

            // Assert
            action.ShouldThrow<ForbiddenException>();
        }
    }
}
