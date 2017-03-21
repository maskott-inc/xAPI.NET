using System;
using xAPI.Client.Endpoints;

namespace xAPI.Client
{
    /// <summary>
    /// The IXApiClient interface exposes the different resources
    /// defined by the xAPI specification.
    /// </summary>
    public interface IXApiClient : IDisposable
    {
        /// <summary>
        /// The statements resource.
        /// Add/retrieve statements to/from the LRS.
        /// </summary>
        IStatementsApi Statements { get; }

        /// <summary>
        /// The states resource.
        /// Manage activity states.
        /// </summary>
        IStatesApi States { get; }

        /// <summary>
        /// The agents resource.
        /// Retrieve agent definitions.
        /// </summary>
        IAgentsApi Agents { get; }

        /// <summary>
        /// The activities resource.
        /// Retrieve activity definitions.
        /// </summary>
        IActivitiesApi Activities { get; }

        /// <summary>
        /// The agent profiles resource.
        /// Manage agent profiles.
        /// </summary>
        IAgentProfilesApi AgentProfiles { get; }

        /// <summary>
        /// The activity profiles resource.
        /// Manage activity profiles.
        /// </summary>
        IActivityProfilesApi ActivityProfiles { get; }

        /// <summary>
        /// The about resource.
        /// Base information about the LRS API.
        /// </summary>
        IAboutApi About { get; }
    }
}
