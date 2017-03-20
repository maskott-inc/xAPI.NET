using System.Net;

namespace xAPI.Client.Exceptions
{
    /// <summary>
    /// HttpException thrown when receiving an out of specification status code from the LRS.
    /// </summary>
    public class UnexpectedHttpException : HttpException
    {
        /// <summary>
        /// The status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// The reason associated with the status code.
        /// </summary>
        public string ReasonPhrase { get; set; }

        /// <summary>
        /// Creates a new instance of UnexpectedHttpException.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="reasonPhrase">The reason associated with the status code.</param>
        public UnexpectedHttpException(HttpStatusCode statusCode, string reasonPhrase)
            : base($"Unexpected HTTP status code {statusCode} in LRS response. Reason: {reasonPhrase}.")
        {
            this.StatusCode = statusCode;
            this.ReasonPhrase = reasonPhrase;
        }
    }
}
