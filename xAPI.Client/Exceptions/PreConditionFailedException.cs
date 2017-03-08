using System;

namespace xAPI.Client.Exceptions
{
    public class PreConditionFailedException : Exception
    {
        public PreConditionFailedException()
        {
        }

        public PreConditionFailedException(string message) : base(message)
        {
        }
    }
}
