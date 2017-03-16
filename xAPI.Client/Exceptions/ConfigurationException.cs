using System;

namespace xAPI.Client.Exceptions
{
    public class ConfigurationException : XApiException
    {
        public ConfigurationException()
        {
        }

        public ConfigurationException(string message) : base(message)
        {
        }

        public ConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
