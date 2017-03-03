using Maskott.xAPI.Client.Requests;
using Maskott.xAPI.Client.Resources;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Endpoints
{
    public interface IAgentsApi
    {
        Task<Person> Get(GetAgentRequest request);
    }
}
