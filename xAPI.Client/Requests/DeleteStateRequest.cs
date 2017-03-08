using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public class DeleteStateRequest : ASingleStateRequest
    {
        public string ETag { get; private set; }

        public static DeleteStateRequest Create()
        {
            return new DeleteStateRequest(etag: null);
        }

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
