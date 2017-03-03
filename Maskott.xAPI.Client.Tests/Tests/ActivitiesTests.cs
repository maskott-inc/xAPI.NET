using FluentAssertions;
using Maskott.xAPI.Client.Configuration;
using Maskott.xAPI.Client.Exceptions;
using Maskott.xAPI.Client.Requests;
using Maskott.xAPI.Client.Resources;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Tests.Tests
{
    public class ActivitiesTests : BaseTest
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
            this._client = XApi.CreateUsingBasicHttpAuthenticator(config);
        }

        [TearDown]
        public void TearDownAbout()
        {
            this._client.Dispose();
        }

        [Test]
        public async Task can_get_activity_definition()
        {
            var request = new GetActivityRequest()
            {
                ActivityId = new Uri("http://www.example.org/activity")
            };
            Activity activity = await this._client.Activities.Get(request);
            activity.Should().NotBeNull();
            activity.Id.Should().Be(request.ActivityId);
            activity.Definition.Should().NotBeNull();
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
            using (IXApiClient client = XApi.CreateUsingBasicHttpAuthenticator(config))
            {
                var request = new GetActivityRequest()
                {
                    ActivityId = new Uri("http://www.example.org/activity")
                };
                Action action = async () => await this._client.Activities.Get(request);
                action.ShouldThrow<ForbiddenException>();
            }
        }
    }
}
