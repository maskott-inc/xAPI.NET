using Maskott.xAPI.Client.Authenticators;
using Maskott.xAPI.Client.Configuration;
using Maskott.xAPI.Client.Endpoints;
using Maskott.xAPI.Client.Endpoints.Impl;
using Maskott.xAPI.Client.Exceptions;
using Maskott.xAPI.Client.Resources;
using System;

namespace Maskott.xAPI.Client
{
    internal class XApiClient : IXApiClient
    {
        private readonly HttpClientWrapper _httpClientWrapper;

        public XApiClient()
        {
            this._httpClientWrapper = new HttpClientWrapper();
            this._statements = new StatementsApi(this._httpClientWrapper);
            this._states = new StatesApi(this._httpClientWrapper);
            this._agents = new AgentsApi(this._httpClientWrapper);
            this._activities = new ActivitiesApi(this._httpClientWrapper);
            this._agentProfiles = new AgentProfilesApi(this._httpClientWrapper);
            this._activityProfiles = new ActivityProfilesApi(this._httpClientWrapper);
            this._about = new AboutApi(this._httpClientWrapper);
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

            this._httpClientWrapper.HttpClient = configuration.HttpClient;
            this._httpClientWrapper.HttpClient.BaseAddress = configuration.EndpointUri;
            this._httpClientWrapper.HttpClient.DefaultRequestHeaders.Add("X-Experience-API-Version", configuration.Version.ToString());
        }

        public void SetAuthenticator(ILRSAuthenticator authenticator)
        {
            this._httpClientWrapper.Authenticator = authenticator;
        }

        private void EnsureConfigured()
        {
            if (this._httpClientWrapper.HttpClient == null)
            {
                throw new ConfigurationException($"xAPI client is not configured. Please call the {nameof(SetConfiguration)} method before accessing resources.");
            }
            if (this._httpClientWrapper.Authenticator == null)
            {
                throw new ConfigurationException($"xAPI client is not configured. Please call the {nameof(SetAuthenticator)} method before accessing resources.");
            }
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

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this._httpClientWrapper.HttpClient.Dispose();
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
