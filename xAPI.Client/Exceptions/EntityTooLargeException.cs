using System;

namespace xAPI.Client.Exceptions
{
    public class EntityTooLargeException : HttpException
    {
        public EntityTooLargeException()
        {
        }

        public EntityTooLargeException(string message) : base(message)
        {
        }

        public EntityTooLargeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
