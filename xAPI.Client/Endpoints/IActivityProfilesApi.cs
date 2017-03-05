using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    public interface IActivityProfilesApi
    {
        Task<ActivityProfileDocument> Get(GetActivityProfileRequest request);
        Task Put(PutActivityProfileRequest request, ActivityProfileDocument activityProfile);
        Task Post(PostActivityProfileRequest request, ActivityProfileDocument activityProfile);
        Task Delete(DeleteActivityProfileRequest request);
        Task<List<string>> GetMany(GetActivityProfilesRequest request);
    }
}
