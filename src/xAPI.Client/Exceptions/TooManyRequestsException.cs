namespace xAPI.Client.Exceptions
{
    /// <summary>
    /// HttpException thrown when receiving a 429 status code from the LRS.
    /// </summary>
    public class TooManyRequestsException : HttpException
    {
        /// <summary>
        /// Creates a new instance of HttpException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public TooManyRequestsException(string message) : base(message)
        {
        }
    }
}
