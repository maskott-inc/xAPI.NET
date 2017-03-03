using Maskott.xAPI.Client.Authenticators;
using Maskott.xAPI.Client.Common;
using Maskott.xAPI.Client.Configuration;
using Maskott.xAPI.Client.Endpoints;
using Maskott.xAPI.Client.Endpoints.Impl;
using Maskott.xAPI.Client.Exceptions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client
{
    internal class XApiClient : IXApiClient, IHttpClientWrapper
    {
        private Uri _endpoint;
        private XApiVersion _version;
        private HttpClient _client;
        private ILRSAuthenticator _authenticator;

        public XApiClient()
        {
            this._statements = new StatementsApi(this);
            this._states = new StatesApi(this);
            this._agents = new AgentsApi(this);
            this._activities = new ActivitiesApi(this);
            this._agentProfiles = new AgentProfilesApi(this);
            this._activityProfiles = new ActivityProfilesApi(this);
            this._about = new AboutApi(this);
        }

        public void SetConfiguration(EndpointConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (configuration.EndpointUri == null || !configuration.EndpointUri.IsAbsoluteUri)
            {
                throw new ArgumentException("The endpoint must be a valid absolute URI");
            }

            if (configuration.Version == null || !configuration.Version.IsSupported())
            {
                string supportedVersions = string.Join(", ", XApiVersion.SUPPORTED_VERSIONS);
                throw new ArgumentException($"Version is not supported. Supported versions are: {supportedVersions}");
            }

            this._endpoint = configuration.EndpointUri;
            this._version = configuration.Version;

            if (configuration.Handler != null)
            {
                this._client = new HttpClient(configuration.Handler);
            }
            else
            {
                this._client = new HttpClient();
            }
            this._client.BaseAddress = configuration.EndpointUri;
            this._client.DefaultRequestHeaders.Add("X-Experience-API-Version", configuration.Version.ToString());
        }

        private void EnsureConfigured()
        {
            if (this._client == null)
            {
                throw new ConfigurationException($"xAPI client is not configured. Please call the {nameof(SetConfiguration)} method before accessing resources.");
            }
        }

        public void SetAuthenticator(ILRSAuthenticator authenticator)
        {
            this._authenticator = authenticator;
        }

        #region IXApiClient members

        private readonly IStatementsApi _statements;
        IStatementsApi IXApiClient.Statements
        {
            get
            {
                this.EnsureConfigured();
                return this._statements;
            }
        }

        private readonly IStatesApi _states;
        IStatesApi IXApiClient.States
        {
            get
            {
                this.EnsureConfigured();
                return this._states;
            }
        }

        private readonly IAgentsApi _agents;
        IAgentsApi IXApiClient.Agents
        {
            get
            {
                this.EnsureConfigured();
                return this._agents;
            }
        }

        private readonly IActivitiesApi _activities;
        IActivitiesApi IXApiClient.Activities
        {
            get
            {
                this.EnsureConfigured();
                return this._activities;
            }
        }

        private readonly IAgentProfilesApi _agentProfiles;
        IAgentProfilesApi IXApiClient.AgentProfiles
        {
            get
            {
                this.EnsureConfigured();
                return this._agentProfiles;
            }
        }

        private readonly IActivityProfilesApi _activityProfiles;
        IActivityProfilesApi IXApiClient.ActivityProfiles
        {
            get
            {
                this.EnsureConfigured();
                return this._activityProfiles;
            }
        }

        private readonly IAboutApi _about;
        IAboutApi IXApiClient.About
        {
            get
            {
                this.EnsureConfigured();
                return this._about;
            }
        }

        #endregion

        #region IHttpClientWrapper members

        async Task<T> IHttpClientWrapper.Get<T>(string url)
        {
            return await this.Get<T>(url);
        }

        private async Task<T> Get<T>(string url)
        {
            // Handle specific headers
            this.ClearSpecificRequestHeaders();

            // Ensure authorization is valid
            await this.SetAuthorizationHeader();

            // Perform request
            HttpResponseMessage response = await this._client.GetAsync(url);

            // Handle response
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                // Credentials are valid but user is not allowed to
                // perform this operation on this resource
                throw new ForbiddenException("Invalid xAPI credentials");
            }
            else
            {
                response.EnsureSuccessStatusCode();
            }

            // Parse content
            return await response.Content.ReadAsAsync<T>();
        }

        private void ClearSpecificRequestHeaders()
        {
            this._client.DefaultRequestHeaders.Authorization = null;
            this._client.DefaultRequestHeaders.IfMatch.Clear();
            this._client.DefaultRequestHeaders.IfNoneMatch.Clear();
        }

        private async Task SetAuthorizationHeader()
        {
            AuthorizationHeaderInfos authorization = await this._authenticator.GetAuthorization();
            if (authorization != null)
            {
                this._client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    authorization.Scheme, authorization.Parameter
                );
            }
            else
            {
                this._client.DefaultRequestHeaders.Authorization = null;
            }
        }

        private void SetIfMatchHeader(string etag)
        {
            if (!string.IsNullOrEmpty(etag))
            {
                this._client.DefaultRequestHeaders.IfMatch.Add(new EntityTagHeaderValue(etag));
            }
        }

        private void SetIfNoneMatchHeader(string etag)
        {
            if (string.IsNullOrEmpty(etag))
            {
                this._client.DefaultRequestHeaders.IfNoneMatch.Add(new EntityTagHeaderValue("*"));
            }
            else
            {
                this._client.DefaultRequestHeaders.IfNoneMatch.Add(new EntityTagHeaderValue(etag));
            }
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this._client.Dispose();
                }

                this.disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
        }

        #endregion
    }
}
