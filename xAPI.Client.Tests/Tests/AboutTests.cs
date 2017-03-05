using FluentAssertions;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using xAPI.Client.Configuration;
using xAPI.Client.Resources;

namespace xAPI.Client.Tests.Tests
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
                Version = Config.Version,
                HttpClient = this.GetHttpClient()
            };
            this._client = XApiClientFactory.CreateUsingAnonymousAuthenticator(config);
        }

        //TODO: refactor HTTP client mocking in base test class
        //TODO: select file using test method name
        private HttpClient GetHttpClient()
        {
            if (!Config.MockHttpClient)
            {
                return null;
            }

            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .When(HttpMethod.Get, $"{Config.EndpointUri}about")
                .Respond(HttpStatusCode.OK, "application/json", File.ReadAllText(TestContext.CurrentContext.TestDirectory + @"\Data\about\get.json"));
            return mockHttp.ToHttpClient();
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
