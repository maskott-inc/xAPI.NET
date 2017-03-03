using FluentAssertions;
using Maskott.xAPI.Client.Configuration;
using Maskott.xAPI.Client.Resources;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Tests.Tests
{
    public class AboutTests : BaseTest
    {
        private IXApiClient _client;

        [SetUp]
        public void SetUpAbout()
        {
            var config = new AnonymousEndpointConfiguration()
            {
                EndpointUri = Config.EndpointUri,
                Version = Config.Version
            };
            this._client = XApi.CreateUsingAnonymousAuthenticator(config);
        }

        [TearDown]
        public void TearDownAbout()
        {
            this._client.Dispose();
        }

        [Test]
        public async Task can_get_about_resource_with_dynamic_extensions()
        {
            About about = await this._client.About.Get();
            about.Should().NotBeNull();
            about.Versions.Should().HaveCount(x => x > 0);
        }

        [Test]
        public async Task can_get_about_resource_with_object_extensions()
        {
            About<object> about = await this._client.About.Get<object>();
            about.Should().NotBeNull();
            about.Versions.Should().HaveCount(x => x > 0);
        }
    }
}
