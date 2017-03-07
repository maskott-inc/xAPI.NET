using System.Collections.Generic;
using System.Threading.Tasks;
using xAPI.Client.Requests;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    public interface IStatesApi
    {
        Task<StateDocument> Get(GetStateRequest request);
        Task<StateDocument<T>> Get<T>(GetStateRequest request);

        Task Put<T>(PutStateRequest request, StateDocument<T> state);

        Task Post<T>(PostStateRequest request, StateDocument<T> state);

        Task Delete(DeleteStateRequest request);

        Task<List<string>> GetMany(GetStatesRequest request);

        Task DeleteMany(DeleteStatesRequest request);
    }
}
