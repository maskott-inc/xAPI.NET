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

        Task<bool> Put<T>(PutStateRequest<T> request);

        Task<bool> Post<T>(PostStateRequest<T> request);

        Task<bool> Delete(DeleteStateRequest request);

        Task<List<string>> GetMany(GetStatesRequest request);

        Task<bool> DeleteMany(DeleteStatesRequest request);
    }
}
