using Maskott.xAPI.Client.Endpoints;
using System;

namespace Maskott.xAPI.Client
{
    public interface IXApiClient : IDisposable
    {
        IStatementsApi Statements { get; }
        IStatesApi States { get; }
        IAgentsApi Agents { get; }
        IActivitiesApi Activities { get; }
        IAgentProfilesApi AgentProfiles { get; }
        IActivityProfilesApi ActivityProfiles { get; }
        IAboutApi About { get; }
    }
}
