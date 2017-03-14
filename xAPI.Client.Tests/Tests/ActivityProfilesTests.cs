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
    public class ActivityProfilesTests : BaseEndpointTest
    {
        private const string ACTIVITY_ID = "http://www.example.org/activity";
        private static readonly string PROFILE_ID = Guid.NewGuid().ToString();
        private static readonly DateTimeOffset SINCE = DateTimeOffset.UtcNow.AddDays(-1);
        private static readonly DateTimeOffset LAST_MODIFIED = DateTimeOffset.UtcNow;
        private const string ETAG = "\"123456789\"";

        [Test]
        public async Task can_get_activity_profile_with_dynamic_document()
        {
            // Arrange
            var request = new GetActivityProfileRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID),
                ProfileId = PROFILE_ID
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("profileId", PROFILE_ID)
                .Respond(this.GetActivityProfileResponseMessage());

            // Act
            ActivityProfileDocument activityProfile = await this._client.ActivityProfiles.Get(request);

            // Assert
            activityProfile.Should().NotBeNull();
            activityProfile.ETag.Should().Be(ETAG);
            activityProfile.LastModified.Should().Be(LAST_MODIFIED);
            string content = activityProfile.Content.ToObject<string>();
            content.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task can_get_activity_profile_with_string_document()
        {
            // Arrange
            var request = new GetActivityProfileRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID),
                ProfileId = PROFILE_ID
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("profileId", PROFILE_ID)
                .Respond(this.GetActivityProfileResponseMessage());

            // Act
            ActivityProfileDocument<string> activityProfile = await this._client.ActivityProfiles.Get<string>(request);

            // Assert
            activityProfile.Should().NotBeNull();
            activityProfile.ETag.Should().Be(ETAG);
            activityProfile.LastModified.Should().Be(LAST_MODIFIED);
            activityProfile.Content.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void cannot_get_activity_profile_when_unauthorized()
        {
            // Arrange
            var request = new GetActivityProfileRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID),
                ProfileId = PROFILE_ID
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("profileId", PROFILE_ID)
                .Respond(HttpStatusCode.Forbidden);

            // Act
            Func<Task> action = async () =>
            {
                await this._client.ActivityProfiles.Get(request);
            };

            // Assert
            action.ShouldThrow<ForbiddenException>();
        }

        [Test]
        public async Task can_put_new_activity_profile()
        {
            // Arrange
            var state = new ActivityProfileDocument<string>()
            {
                Content = "foo"
            };
            var request = PutActivityProfileRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-None-Match", "*")
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.ActivityProfiles.Put(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task can_put_existing_activity_profile()
        {
            // Arrange
            var state = new ActivityProfileDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = PutActivityProfileRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.ActivityProfiles.Put(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task cannot_put_existing_activity_profile_when_etag_is_invalid()
        {
            // Arrange
            var state = new ActivityProfileDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = PutActivityProfileRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Put, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.PreconditionFailed);

            // Act
            bool result = await this._client.ActivityProfiles.Put(request);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task can_post_new_activity_profile()
        {
            // Arrange
            var state = new ActivityProfileDocument<string>()
            {
                Content = "foo"
            };
            var request = PostActivityProfileRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-None-Match", "*")
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.ActivityProfiles.Post(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task can_post_existing_activity_profile()
        {
            // Arrange
            var state = new ActivityProfileDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = PostActivityProfileRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.ActivityProfiles.Post(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task cannot_post_existing_activity_profile_when_etag_is_invalid()
        {
            // Arrange
            var state = new ActivityProfileDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = PostActivityProfileRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Post, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.PreconditionFailed);

            // Act
            bool result = await this._client.ActivityProfiles.Post(request);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task can_delete_existing_activity_profile()
        {
            // Arrange
            var state = new ActivityProfileDocument<string>()
            {
                Content = "foo"
            };
            var request = DeleteActivityProfileRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Delete, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("profileId", PROFILE_ID)
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.ActivityProfiles.Delete(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task can_delete_existing_activity_profile_with_etag()
        {
            // Arrange
            var state = new ActivityProfileDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = DeleteActivityProfileRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Delete, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.NoContent);

            // Act
            bool result = await this._client.ActivityProfiles.Delete(request);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task cannot_delete_existing_activity_profile_with_invalid_etag()
        {
            // Arrange
            var state = new ActivityProfileDocument<string>()
            {
                Content = "foo",
                ETag = ETAG
            };
            var request = DeleteActivityProfileRequest.Create(state);
            request.ActivityId = new Uri(ACTIVITY_ID);
            request.ProfileId = PROFILE_ID;
            this._mockHttp
                .When(HttpMethod.Delete, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("profileId", PROFILE_ID)
                .WithHeaders("If-Match", ETAG)
                .Respond(HttpStatusCode.PreconditionFailed);

            // Act
            bool result = await this._client.ActivityProfiles.Delete(request);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task can_get_many_activity_profiles()
        {
            // Arrange
            var request = new GetActivityProfilesRequest()
            {
                ActivityId = new Uri(ACTIVITY_ID),
                Since = SINCE
            };
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("activities/profile"))
                .WithQueryString("activityId", ACTIVITY_ID)
                .WithQueryString("since", SINCE.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"))
                .Respond(HttpStatusCode.OK, "application/json", this.ReadDataFile("activities/profile/get_many.json"));

            // Act
            List<string> stateIds = await this._client.ActivityProfiles.GetMany(request);

            // Assert
            stateIds.Should().NotBeNullOrEmpty();
        }

        private HttpResponseMessage GetActivityProfileResponseMessage()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.ETag = new EntityTagHeaderValue(ETAG);
            response.Content = new StringContent(this.ReadDataFile("activities/profile/get.json"), Encoding.UTF8, "application/json");
            response.Content.Headers.LastModified = LAST_MODIFIED;

            return response;
        }
    }
}
