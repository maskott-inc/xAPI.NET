using System;

namespace xAPI.Client.Exceptions
{
    public class TooManyRequestsException : HttpException
    {
        public TooManyRequestsException()
        {
        }

        public TooManyRequestsException(string message) : base(message)
        {
        }

        public TooManyRequestsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
