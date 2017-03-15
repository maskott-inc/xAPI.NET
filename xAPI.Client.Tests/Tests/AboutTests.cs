using FluentAssertions;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using xAPI.Client.Configuration;
using xAPI.Client.Resources;

namespace xAPI.Client.Tests
{
    public class AboutTests : BaseEndpointTest
    {
        protected override EndpointConfiguration GetEndpointConfiguration()
        {
            return new AnonymousEndpointConfiguration();
        }

        [Test]
        public async Task can_get_about_resource()
        {
            // Arrange
            this._mockHttp
                .When(HttpMethod.Get, this.GetApiUrl("about"))
                .WithHeaders("X-Experience-API-Version", VERSION)
                .Respond(HttpStatusCode.OK, "application/json", this.ReadDataFile("about/get.json"));

            // Act
            About about = await this._client.About.Get();

            // Assert
            about.Should().NotBeNull();
            about.Versions.Should().HaveCount(x => x > 0);
            about.Extensions.Should().NotBeNull().And.NotBeEmpty();
        }
    }
}
