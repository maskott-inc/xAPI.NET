using Maskott.xAPI.Client.Requests;
using Maskott.xAPI.Client.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Endpoints
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
