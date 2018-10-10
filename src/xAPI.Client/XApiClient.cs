using xAPI.Client.Configuration;
using xAPI.Client.Endpoints;
using xAPI.Client.Endpoints.Impl;
using xAPI.Client.Http;

namespace xAPI.Client
{
    public class XApiClient : IXApiClient
    {
        private readonly HttpClientWrapper _httpClientWrapper;

        public XApiClient(EndpointConfiguration configuration)
        {
            this._httpClientWrapper = new HttpClientWrapper(configuration);
            this.Statements = new StatementsApi(this._httpClientWrapper);
            this.States = new StatesApi(this._httpClientWrapper);
            this.Agents = new AgentsApi(this._httpClientWrapper);
            this.Activities = new ActivitiesApi(this._httpClientWrapper);
            this.AgentProfiles = new AgentProfilesApi(this._httpClientWrapper);
            this.ActivityProfiles = new ActivityProfilesApi(this._httpClientWrapper);
            this.About = new AboutApi(this._httpClientWrapper);
        }

        public IStatementsApi Statements { get; }
        public IStatesApi States { get; }
        public IAgentsApi Agents { get; }
        public IActivitiesApi Activities { get; }
        public IAgentProfilesApi AgentProfiles { get; }
        public IActivityProfilesApi ActivityProfiles { get; }
        public IAboutApi About { get; }

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this._httpClientWrapper.Dispose();
                }

                this.disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
        }
    }
}
