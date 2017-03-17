using System.Net.Http.Headers;
using System.Threading.Tasks;
using xAPI.Client.Http.Options;

namespace xAPI.Client.Http
{
    internal interface IHttpClientWrapper
    {
        Task<HttpResult<T>> GetJson<T>(RequestOptions options);
        Task<HttpResult> PutJson<T>(RequestOptions options, T content);
        Task<HttpResult> PostJson<T>(RequestOptions options, T content);
        Task<HttpResult> Delete(RequestOptions options);
    }

    internal class HttpResult
    {
        public HttpResponseHeaders Headers { get; set; }
    }

    internal class HttpResult<T> : HttpResult
    {
        public HttpContentHeaders ContentHeaders { get; set; }
        public T Content { get; set; }
    }
}
