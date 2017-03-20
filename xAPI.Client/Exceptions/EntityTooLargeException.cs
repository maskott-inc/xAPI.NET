namespace xAPI.Client.Exceptions
{
    /// <summary>
    /// HttpException thrown when receiving a 413 status code from the LRS.
    /// </summary>
    public class EntityTooLargeException : HttpException
    {
        /// <summary>
        /// Creates a new instance of HttpException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public EntityTooLargeException(string message) : base(message)
        {
        }
    }
}
