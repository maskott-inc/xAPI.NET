using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Class used to perform a GET request on the activities resource.
    /// </summary>
    public class GetActivityRequest : ARequest
    {
        /// <summary>
        /// Creates a new instance of GetActivityRequest.
        /// </summary>
        public GetActivityRequest()
        {
        }

        /// <summary>
        /// Creates a new instance of GetActivityRequest using the
        /// specified activity to initialize its state.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public GetActivityRequest(Activity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException(nameof(activity));
            }

            this.ActivityId = activity.Id;
        }

        /// <summary>
        /// Gets or sets the activity ID.
        /// </summary>
        public Uri ActivityId { get; set; }

        internal override void Validate()
        {
            if (this.ActivityId == null)
            {
                throw new ArgumentNullException(nameof(this.ActivityId));
            }
            if (!this.ActivityId.IsAbsoluteUri)
            {
                throw new ArgumentException("IRI should be absolute", nameof(this.ActivityId));
            }
        }
    }
}
