using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Factory used to create requests to perform a DELETE request on the
    /// activity profiles resource.
    /// </summary>
    public class DeleteActivityProfileRequest : ASingleActivityProfileRequest
    {
        internal string ETag { get; private set; }

        /// <summary>
        /// Creates a new request instance without concurrency management.
        /// </summary>
        /// <returns></returns>
        public static DeleteActivityProfileRequest Create()
        {
            return new DeleteActivityProfileRequest(etag: null);
        }

        /// <summary>
        /// Creates a new request instance using an existing document
        /// to handle concurrency operations.
        /// </summary>
        /// <param name="existingActivityProfile">The document used for concurrency comparisons.</param>
        /// <returns></returns>
        public static DeleteActivityProfileRequest Create<T>(ActivityProfileDocument<T> existingActivityProfile)
        {
            return new DeleteActivityProfileRequest(existingActivityProfile?.ETag);
        }

        internal DeleteActivityProfileRequest(string etag)
        {
            this.ETag = etag;
        }
    }
}
