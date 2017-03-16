using NUnit.Framework;
using RichardSzalay.MockHttp;
using System;
using xAPI.Client.Configuration;
using xAPI.Client.Resources;

namespace xAPI.Client.Tests
{
    public abstract class BaseEndpointTest : BaseTest
    {
        protected const string ENDPOINT_URI = "https://www.example.org/xAPI/";
        protected const string VERSION = "1.0.3";
        protected const string BASIC_HTTP_USERNAME = "test_username";
        protected const string BASIC_HTTP_PASSWORD = "test_password";
        protected const string OAUTH_CLIENT_ID = "test_client_id";
        protected const string OAUTH_CLIENT_SECRET = "test_client_secret";

        protected MockHttpMessageHandler _mockHttp;
        protected IXApiClient _client;

        [SetUp]
        public void SetUpHttpClient()
        {
            this._mockHttp = new MockHttpMessageHandler();
            EndpointConfiguration config = this.GetEndpointConfiguration();
            config.EndpointUri = new Uri(ENDPOINT_URI);
            config.Version = XApiVersion.Parse(VERSION);
            config.HttpClient = this._mockHttp.ToHttpClient();
            this._client = XApiClientFactory.Create(config);
        }

        [TearDown]
        public void TearDownHttpClient()
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

        protected string GetApiUrl(string endpoint)
        {
            var baseUrl = new Uri(ENDPOINT_URI);
            var url = new Uri(baseUrl, endpoint);
            return url.ToString();
        }
    }
}
