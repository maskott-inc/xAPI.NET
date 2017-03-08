using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    public interface IActivityProfilesApi
    {
        Task<ActivityProfileDocument> Get(GetActivityProfileRequest request);
        Task<ActivityProfileDocument<T>> Get<T>(GetActivityProfileRequest request);

        Task<bool> Put<T>(PutActivityProfileRequest<T> request);

        Task<bool> Post<T>(PostActivityProfileRequest<T> request);

        Task<bool> Delete(DeleteActivityProfileRequest request);

        Task<List<string>> GetMany(GetActivityProfilesRequest request);
    }
}
