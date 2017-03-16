using System;

namespace xAPI.Client.Exceptions
{
    public class ForbiddenException : HttpException
    {
        public ForbiddenException()
        {
        }

        public ForbiddenException(string message) : base(message)
        {
        }

        public ForbiddenException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
