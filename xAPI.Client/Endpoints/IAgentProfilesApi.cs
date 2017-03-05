using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
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
