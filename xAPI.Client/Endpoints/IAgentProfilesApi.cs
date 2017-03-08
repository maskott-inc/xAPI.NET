using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    public interface IAgentProfilesApi
    {
        Task<AgentProfileDocument> Get(GetAgentProfileRequest request);
        Task<AgentProfileDocument<T>> Get<T>(GetAgentProfileRequest request);

        Task<bool> Put<T>(PutAgentProfileRequest<T> request);

        Task<bool> Post<T>(PostAgentProfileRequest<T> request);

        Task<bool> Delete(DeleteAgentProfileRequest request);

        Task<List<string>> GetMany(GetAgentProfilesRequest request);
    }
}
