namespace xAPI.Client.Exceptions
{
    /// <summary>
    /// HttpException thrown when receiving a 403 status code from the LRS.
    /// </summary>
    public class ForbiddenException : HttpException
    {
        /// <summary>
        /// Creates a new instance of HttpException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public ForbiddenException(string message) : base(message)
        {
        }
    }
}
