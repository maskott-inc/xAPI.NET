using System.Net.Http;
using System.Threading.Tasks;
using xAPI.Client.Http.Options;

namespace xAPI.Client.Http
{
    internal interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetJson(RequestOptions options);
        Task<HttpResponseMessage> PutJson<T>(RequestOptions options, T content);
        Task<HttpResponseMessage> PostJson<T>(RequestOptions options, T content);
        Task<HttpResponseMessage> Delete(RequestOptions options);
    }
}
