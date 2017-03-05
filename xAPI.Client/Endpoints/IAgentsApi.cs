using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    public interface IAgentsApi
    {
        Task<Person> Get(GetAgentRequest request);
    }
}
