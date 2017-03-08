using System;

namespace xAPI.Client.Exceptions
{
    public class EntityTooLargeException : Exception
    {
        public EntityTooLargeException()
        {
        }

        public EntityTooLargeException(string message) : base(message)
        {
        }
    }
}
