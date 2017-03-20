namespace xAPI.Client.Exceptions
{
    /// <summary>
    /// HttpException thrown when receiving a 412 status code from the LRS.
    /// </summary>
    public class PreConditionFailedException : HttpException
    {
        /// <summary>
        /// Creates a new instance of HttpException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public PreConditionFailedException(string message) : base(message)
        {
        }
    }
}
