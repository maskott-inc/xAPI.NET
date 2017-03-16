using System.Net;

namespace xAPI.Client.Exceptions
{
    public class UnexpectedHttpException : HttpException
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }

        public UnexpectedHttpException(HttpStatusCode statusCode, string reasonPhrase)
            : base($"Unexpected HTTP status code {statusCode} in LRS response. Reason: {reasonPhrase}.")
        {
            this.StatusCode = statusCode;
            this.ReasonPhrase = reasonPhrase;
        }
    }
}
