using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public class DeleteAgentProfileRequest : ASingleAgentProfileRequest
    {
        public string ETag { get; private set; }

        public static DeleteAgentProfileRequest Create()
        {
            return new DeleteAgentProfileRequest(etag: null);
        }

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
