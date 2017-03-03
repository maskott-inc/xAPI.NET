using System.Threading.Tasks;

namespace Maskott.xAPI.Client
{
    internal interface IHttpClientWrapper
    {
        Task<T> Get<T>(string url);
    }
}
