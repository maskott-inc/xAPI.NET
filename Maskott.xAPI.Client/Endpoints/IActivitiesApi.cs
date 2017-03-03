using Maskott.xAPI.Client.Requests;
using Maskott.xAPI.Client.Resources;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Endpoints
{
    public interface IActivitiesApi
    {
        Task<Activity> Get(GetActivityRequest request);
        Task<Activity<T>> Get<T>(GetActivityRequest request);
    }
}
