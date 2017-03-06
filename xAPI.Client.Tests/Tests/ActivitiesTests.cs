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
    public class ActivitiesTests : BaseTest
    {
        [Test]
        public async Task can_get_activity_definition()
        {
            // Arrange
            var request = new GetActivityRequest()
            {
                ActivityId = new Uri("http://www.example.org/activity")
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities"))
                .Respond(HttpStatusCode.OK, "application/json", this.ReadDataFile("activities/get.json"));

            // Act
            Activity activity = await this._client.Activities.Get(request);

            // Assert
            activity.Should().NotBeNull();
            activity.Id.Should().Be(request.ActivityId);
            activity.Definition.Should().NotBeNull();
        }

        [Test]
        public void cannot_get_activity_definition_if_unauthorized()
        {
            // Arrange
            var request = new GetActivityRequest()
            {
                ActivityId = new Uri("http://www.example.org/activity")
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities"))
                .Respond(HttpStatusCode.Forbidden);

            // Act
            Func<Task> action = async () =>
            {
                await this._client.Activities.Get(request);
            };

            // Assert
            action.ShouldThrow<ForbiddenException>();
        }
    }
}
