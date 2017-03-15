using System.Threading.Tasks;
using xAPI.Client.Resources;

namespace xAPI.Client.Http
{
    internal interface IHttpClientWrapper
    {
        Task<T> GetJson<T>(string url, GetJsonOptions options);
        Task PutJson<T>(string url, PutJsonOptions options, T content);
        Task PostJson<T>(string url, PostJsonOptions options, T content);

        Task GetJsonDocument<T>(string url, GetJsonDocumentOptions options, BaseDocument<T> document);
        Task PutJsonDocument<T>(string url, PutJsonDocumentOptions options, BaseDocument<T> document);
        Task PostJsonDocument<T>(string url, PostJsonDocumentOptions options, BaseDocument<T> document);

        Task Delete(string url, DeleteOptions options);
    }
}
