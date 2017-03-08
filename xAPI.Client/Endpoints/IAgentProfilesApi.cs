using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    public interface IAgentProfilesApi
    {
        Task<AgentProfileDocument> Get(GetAgentProfileRequest request);
        Task Put<T>(PutAgentProfileRequest<T> request);
        Task Post<T>(PostAgentProfileRequest<T> request);
        Task Delete(DeleteAgentProfileRequest request);
        Task<List<string>> GetMany(GetAgentProfilesRequest request);
    }
}
