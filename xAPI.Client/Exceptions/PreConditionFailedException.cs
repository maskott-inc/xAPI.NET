using System;

namespace xAPI.Client.Exceptions
{
    public class PreConditionFailedException : HttpException
    {
        public PreConditionFailedException()
        {
        }

        public PreConditionFailedException(string message) : base(message)
        {
        }

        public PreConditionFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
