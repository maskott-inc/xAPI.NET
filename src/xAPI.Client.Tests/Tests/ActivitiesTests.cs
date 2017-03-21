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
using xAPI.Client.Tests.Data;

namespace xAPI.Client.Tests
{
    public class ActivitiesTests : BaseEndpointTest
    {
        private const string ACTIVITY_ID = "http://www.example.org/activity";

        [Test]
        public async Task can_get_activity()
        {
            // Arrange
            var request = new GetActivityRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID)
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .Respond(HttpStatusCode.OK, "application/json", this.ReadDataFile(Constants.ACTIVITY));

            // Act
            Activity activity = await this._client.Activities.Get(request);

            // Assert
            activity.Should().NotBeNull();
            activity.Id.Should().Be(request.ActivityId);
            activity.Definition.Should().NotBeNull();
            activity.Definition.Extensions.Should().NotBeNull().And.NotBeEmpty();
        }

        [Test]
        public async Task can_request_activity_that_does_not_exist()
        {
            // Arrange
            var request = new GetActivityRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID)
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .Respond(HttpStatusCode.NotFound);

            // Act
            Activity activity = await this._client.Activities.Get(request);

            // Assert
            activity.Should().BeNull();
        }

        [Test]
        public void cannot_get_activity_when_unauthorized()
        {
            // Arrange
            var request = new GetActivityRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID)
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities"))
                .WithQueryString("activityId", ACTIVITY_ID)
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
