using Maskott.xAPI.Client.Requests;
using Maskott.xAPI.Client.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Endpoints
{
    public interface IAgentProfilesApi
    {
        Task<AgentProfileDocument> Get(GetAgentProfileRequest request);
        Task Put(PutAgentProfileRequest request, AgentProfileDocument agentProfile);
        Task Post(PostAgentProfileRequest request, AgentProfileDocument agentProfile);
        Task Delete(DeleteAgentProfileRequest request);
        Task<List<string>> GetMany(GetAgentProfilesRequest request);
    }
}
