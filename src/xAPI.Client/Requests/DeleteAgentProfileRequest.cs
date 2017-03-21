using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Factory used to create requests to perform a DELETE request on the
    /// agent profiles resource.
    /// </summary>
    public class DeleteAgentProfileRequest : ASingleAgentProfileRequest
    {
        internal string ETag { get; private set; }

        /// <summary>
        /// Creates a new request instance without concurrency management.
        /// </summary>
        /// <returns></returns>
        public static DeleteAgentProfileRequest Create()
        {
            return new DeleteAgentProfileRequest(etag: null);
        }

        /// <summary>
        /// Creates a new request instance using an existing document
        /// to handle concurrency operations.
        /// </summary>
        /// <param name="existingAgentProfile">The document used for concurrency comparisons.</param>
        /// <returns></returns>
        public static DeleteAgentProfileRequest Create<T>(AgentProfileDocument<T> existingAgentProfile)
        {
            return new DeleteAgentProfileRequest(existingAgentProfile?.ETag);
        }

        internal DeleteAgentProfileRequest(string etag)
        {
            this.ETag = etag;
        }
    }
}
