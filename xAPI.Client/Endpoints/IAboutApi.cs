using System.Threading.Tasks;
using xAPI.Client.Resources;

namespace xAPI.Client.Endpoints
{
    public interface IAboutApi
    {
        Task<About> Get();
        Task<About<T>> Get<T>();
    }
}
