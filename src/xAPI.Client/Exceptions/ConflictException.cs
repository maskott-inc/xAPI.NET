namespace xAPI.Client.Exceptions
{
    /// <summary>
    /// HttpException thrown when receiving a 409 status code from the LRS.
    /// </summary>
    public class ConflictException : HttpException
    {
        /// <summary>
        /// Creates a new instance of HttpException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public ConflictException(string message) : base(message)
        {
        }
    }
}
