using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    public interface IStatesApi
    {
        Task<StateDocument> Get(GetStateRequest request);
        Task Put(PutStateRequest request, StateDocument state);
        Task Post(PostStateRequest request, StateDocument state);
        Task Delete(DeleteStateRequest request);
        Task<List<string>> GetMany(GetStatesRequest request);
        Task DeleteMany(DeleteStatesRequest request);
    }
}
