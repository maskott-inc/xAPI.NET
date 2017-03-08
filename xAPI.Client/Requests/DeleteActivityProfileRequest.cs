using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public class DeleteActivityProfileRequest : ASingleActivityProfileRequest
    {
        public string ETag { get; private set; }

        public static DeleteActivityProfileRequest Create()
        {
            return new DeleteActivityProfileRequest(etag: null);
        }

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
