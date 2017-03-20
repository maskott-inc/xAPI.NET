using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Factory used to create requests to perform a PUT request on the
    /// activity profiles resource.
    /// </summary>
    public static class PutActivityProfileRequest
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
        public static PutActivityProfileRequest<T> Create<T>(ActivityProfileDocument<T> activityProfile)
        {
            return new PutActivityProfileRequest<T>(activityProfile);
        }
    }

    /// <summary>
    /// Class used to perform a PUT request on the activity profiles resource.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the document to be stored as JSON in the LRS.
    /// The type must support JSON serialization.
    /// </typeparam>
    public class PutActivityProfileRequest<T> : ASingleActivityProfileRequest
    {
        internal ActivityProfileDocument<T> ActivityProfile { get; private set; }

        internal PutActivityProfileRequest(ActivityProfileDocument<T> activityProfile)
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
