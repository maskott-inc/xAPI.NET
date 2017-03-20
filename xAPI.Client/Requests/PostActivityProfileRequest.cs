using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Factory used to create requests to perform a POST request on the
    /// activity profiles resource.
    /// </summary>
    public static class PostActivityProfileRequest
    {
        /// <summary>
        /// Creates a new request instance using an existing document
        /// to handle concurrency operations.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the document to be stored as JSON in the LRS.
        /// The type must support JSON serialization.
        /// </typeparam>
        /// <param name="activityProfile">The document used for concurrency comparisons.</param>
        /// <returns></returns>
        public static PostActivityProfileRequest<T> Create<T>(ActivityProfileDocument<T> activityProfile)
        {
            return new PostActivityProfileRequest<T>(activityProfile);
        }
    }

    /// <summary>
    /// Class used to perform a POST request on the activity profiles resource.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the document to be stored as JSON in the LRS.
    /// The type must support JSON serialization.
    /// </typeparam>
    public class PostActivityProfileRequest<T> : ASingleActivityProfileRequest
    {
        internal ActivityProfileDocument<T> ActivityProfile { get; private set; }

        internal PostActivityProfileRequest(ActivityProfileDocument<T> activityProfile)
        {
            this.ActivityProfile = activityProfile;
        }

        internal override void Validate()
        {
            base.Validate();

            if (this.ActivityProfile == null)
            {
                throw new ArgumentNullException(nameof(this.ActivityProfile));
            }
        }
    }
}
