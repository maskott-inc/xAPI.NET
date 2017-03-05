using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    public interface IActivitiesApi
    {
        Task<Activity> Get(GetActivityRequest request);
        Task<Activity<T>> Get<T>(GetActivityRequest request);
    }
}
