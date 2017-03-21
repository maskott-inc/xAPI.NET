using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Factory used to create requests to perform a DELETE request on the
    /// activity states resource.
    /// </summary>
    public class DeleteStateRequest : ASingleStateRequest
    {
        internal string ETag { get; private set; }

        /// <summary>
        /// Creates a new request instance without concurrency management.
        /// </summary>
        /// <returns></returns>
        public static DeleteStateRequest Create()
        {
            return new DeleteStateRequest(etag: null);
        }

        /// <summary>
        /// Creates a new request instance using an existing document
        /// to handle concurrency operations.
        /// </summary>
        /// <param name="existingState">The document used for concurrency comparisons.</param>
        /// <returns></returns>
        public static DeleteStateRequest Create<T>(StateDocument<T> existingState)
        {
            return new DeleteStateRequest(existingState?.ETag);
        }

        internal DeleteStateRequest(string etag)
        {
            this.ETag = etag;
        }
    }
}
