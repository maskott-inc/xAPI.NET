using System;

namespace xAPI.Client.Exceptions
{
    public abstract class HttpException : XApiException
    {
        public HttpException()
        {
        }

        public HttpException(string message) : base(message)
        {
        }

        public HttpException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
