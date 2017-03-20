namespace xAPI.Client.Exceptions
{
    /// <summary>
    /// HttpException thrown when receiving a 404 status code from the LRS.
    /// </summary>
    public class NotFoundException : HttpException
    {
        /// <summary>
        /// Creates a new instance of HttpException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
