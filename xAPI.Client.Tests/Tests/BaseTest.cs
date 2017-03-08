using NUnit.Framework;
using RichardSzalay.MockHttp;
using System.IO;
using xAPI.Client.Configuration;

namespace xAPI.Client.Tests
{
    public abstract class BaseTest
    {
        protected MockHttpMessageHandler _mockHttp;
        protected IXApiClient _client;

        [SetUp]
        public void SetUp()
        {
            this._mockHttp = new MockHttpMessageHandler();
            EndpointConfiguration config = this.GetEndpointConfiguration();
            config.EndpointUri = Config.EndpointUri;
            config.Version = Config.Version;
            config.HttpClient = this._mockHttp.ToHttpClient();
            this._client = XApiClientFactory.Create(config);
        }

        [TearDown]
        public void TearDown()
        {
            this._mockHttp = null;
            this._client.Dispose();
        }

        protected virtual EndpointConfiguration GetEndpointConfiguration()
        {
            return new BasicEndpointConfiguration()
            {
                Username = Config.BasicUsername,
                Password = Config.BasicPassword
            };
        }

        protected string ReadDataFile(string file)
        {
            return File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", file));
        }

        protected string GetApiUrl(string endpoint)
        {
            return $"{Config.EndpointUri}{endpoint}";
        }
    }
}
