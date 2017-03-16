using System;

namespace xAPI.Client.Exceptions
{
    public class UnauthorizedException : HttpException
    {
        public UnauthorizedException()
        {
        }

        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
