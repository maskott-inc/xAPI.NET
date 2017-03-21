using System.Net.Http;
using System.Net.Http.Headers;
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
