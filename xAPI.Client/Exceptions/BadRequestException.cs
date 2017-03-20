namespace xAPI.Client.Exceptions
{
    /// <summary>
    /// HttpException thrown when receiving a 400 status code from the LRS.
    /// </summary>
    public class BadRequestException : HttpException
    {
        /// <summary>
        /// Creates a new instance of HttpException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public BadRequestException(string message) : base(message)
        {
        }
    }
}
