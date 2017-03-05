using System;

namespace xAPI.Client.Exceptions
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException()
        {
        }

        public ConfigurationException(string message) : base(message)
        {
        }
    }
}
