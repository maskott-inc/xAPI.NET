using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Factory used to create requests to perform a POST request on the
    /// activity states resource.
    /// </summary>
    public static class PostStateRequest
    {
        /// <summary>
        /// Creates a new request instance using an existing document
        /// to handle concurrency operations.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the document to be stored as JSON in the LRS.
        /// The type must support JSON serialization.
        /// </typeparam>
        /// <param name="state">The document used for concurrency comparisons.</param>
        /// <returns></returns>
        public static PostStateRequest<T> Create<T>(StateDocument<T> state)
        {
            return new PostStateRequest<T>(state);
        }
    }

    /// <summary>
    /// Class used to perform a POST request on the agent profiles resource.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the document to be stored as JSON in the LRS.
    /// The type must support JSON serialization.
    /// </typeparam>
    public class PostStateRequest<T> : ASingleStateRequest
    {
        internal StateDocument<T> State { get; private set; }

        internal PostStateRequest(StateDocument<T> state)
        {
            this.State = state;
        }

        internal override void Validate()
        {
            base.Validate();

            if (this.State == null)
            {
                throw new ArgumentNullException(nameof(this.State));
            }
        }
    }
}
