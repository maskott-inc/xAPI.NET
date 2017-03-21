using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// The base class for any states-related request.
    /// </summary>
    public abstract class AStateRequest : ARequest
    {
        /// <summary>
        /// The Activity id associated with this state.
        /// </summary>
        public Uri ActivityId { get; set; }

        /// <summary>
        /// The Agent associated with this state.
        /// </summary>
        public Agent Agent { get; set; }

        /// <summary>
        /// The registration associated with this state.
        /// </summary>
        public Guid? Registration { get; set; }

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
            if (this.Agent == null)
            {
                throw new ArgumentNullException(nameof(this.Agent));
            }
        }
    }
}
