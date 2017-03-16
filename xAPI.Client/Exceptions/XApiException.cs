using System;

namespace xAPI.Client.Exceptions
{
    public abstract class XApiException : Exception
    {
        public XApiException()
        {
        }

        public XApiException(string message) : base(message)
        {
        }

        public XApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
