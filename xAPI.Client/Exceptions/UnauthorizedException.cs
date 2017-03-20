namespace xAPI.Client.Exceptions
{
    /// <summary>
    /// HttpException thrown when receiving a 401 status code from the LRS.
    /// </summary>
    public class UnauthorizedException : HttpException
    {
        /// <summary>
        /// Creates a new instance of HttpException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
