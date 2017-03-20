namespace xAPI.Client.Exceptions
{
    /// <summary>
    /// Exception thrown when the xAPI client is not properly configured.
    /// </summary>
    public class ConfigurationException : XApiException
    {
        /// <summary>
        /// Creates a new instance of HttpException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public ConfigurationException(string message) : base(message)
        {
        }
    }
}
