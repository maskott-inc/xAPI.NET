using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    public interface IActivityProfilesApi
    {
        Task<ActivityProfileDocument> Get(GetActivityProfileRequest request);
        Task Put<T>(PutActivityProfileRequest<T> request);
        Task Post<T>(PostActivityProfileRequest<T> request);
        Task Delete(DeleteActivityProfileRequest request);
        Task<List<string>> GetMany(GetActivityProfilesRequest request);
    }
}
