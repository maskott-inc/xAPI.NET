using System;
using xAPI.Client.Configuration;
using xAPI.Client.Endpoints;
using xAPI.Client.Endpoints.Impl;

namespace xAPI.Client
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
            this._httpClientWrapper.SetConfiguration(configuration);
        }

        #region IXApiClient members

        private readonly IStatementsApi _statements;
        IStatementsApi IXApiClient.Statements
        {
            get
            {
                this._httpClientWrapper.EnsureConfigured();
                return this._statements;
            }
        }

        private readonly IStatesApi _states;
        IStatesApi IXApiClient.States
        {
            get
            {
                this._httpClientWrapper.EnsureConfigured();
                return this._states;
            }
        }

        private readonly IAgentsApi _agents;
        IAgentsApi IXApiClient.Agents
        {
            get
            {
                this._httpClientWrapper.EnsureConfigured();
                return this._agents;
            }
        }

        private readonly IActivitiesApi _activities;
        IActivitiesApi IXApiClient.Activities
        {
            get
            {
                this._httpClientWrapper.EnsureConfigured();
                return this._activities;
            }
        }

        private readonly IAgentProfilesApi _agentProfiles;
        IAgentProfilesApi IXApiClient.AgentProfiles
        {
            get
            {
                this._httpClientWrapper.EnsureConfigured();
                return this._agentProfiles;
            }
        }

        private readonly IActivityProfilesApi _activityProfiles;
        IActivityProfilesApi IXApiClient.ActivityProfiles
        {
            get
            {
                this._httpClientWrapper.EnsureConfigured();
                return this._activityProfiles;
            }
        }

        private readonly IAboutApi _about;
        IAboutApi IXApiClient.About
        {
            get
            {
                this._httpClientWrapper.EnsureConfigured();
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
                    this._httpClientWrapper.Dispose();
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
