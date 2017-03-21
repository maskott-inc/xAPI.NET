using System;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// The base class for any states-related request, when the request
    /// is associated with a single state.
    /// </summary>
    public abstract class ASingleStateRequest : AStateRequest
    {
        /// <summary>
        /// The id for this state, within the given context.
        /// </summary>
        public string StateId { get; set; }

        internal override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(this.StateId))
            {
                throw new ArgumentNullException(nameof(this.StateId));
            }
        }
    }
}
