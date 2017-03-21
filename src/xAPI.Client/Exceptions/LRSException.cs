namespace xAPI.Client.Exceptions
{
    /// <summary>
    /// The common class for exceptions caused by the LRS (when the LRS
    /// does not conform to the xAPI specification). Note that in some cases,
    /// other exceptions can be thrown when an LRS does not conform to the
    /// specification (HttpException for unexpected status codes, Json.NET
    /// exceptions when deserialization fails...).
    /// </summary>
    public class LRSException : XApiException
    {
        /// <summary>
        /// Creates a new instance of LRSException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public LRSException(string message) : base(message)
        {
        }
    }
}
