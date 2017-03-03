using Maskott.xAPI.Client.Resources;
using System.Threading.Tasks;

namespace Maskott.xAPI.Client.Endpoints
{
    public interface IAboutApi
    {
        Task<About> Get();
        Task<About<T>> Get<T>();
    }
}
