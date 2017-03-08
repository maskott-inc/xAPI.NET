using NUnit.Framework;
using RichardSzalay.MockHttp;
using System;
using System.IO;
using xAPI.Client.Configuration;
using xAPI.Client.Resources;

namespace xAPI.Client.Tests
{
    public abstract class BaseTest
    {
        private const string ENDPOINT_URI = "https://www.example.org/xAPI/";
        private const string VERSION = "1.0.3";
        private const string BASIC_HTTP_USERNAME = "test_username";
        private const string BASIC_HTTP_PASSWORD = "test_password";
        private const string OAUTH_CLIENT_ID = "test_client_id";
        private const string OAUTH_CLIENT_SECRET = "test_client_secret";

        protected MockHttpMessageHandler _mockHttp;
        protected IXApiClient _client;

        [SetUp]
        public void SetUp()
        {
            this._mockHttp = new MockHttpMessageHandler();
            EndpointConfiguration config = this.GetEndpointConfiguration();
            config.EndpointUri = new Uri(ENDPOINT_URI);
            config.Version = XApiVersion.Parse(VERSION);
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
                Username = BASIC_HTTP_USERNAME,
                Password = BASIC_HTTP_PASSWORD
            };
        }

        protected string ReadDataFile(string file)
        {
            return File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", file));
        }

        protected string GetApiUrl(string endpoint)
        {
            return $"{ENDPOINT_URI}{endpoint}";
        }
    }
}
