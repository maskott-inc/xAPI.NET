using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Factory used to create requests to perform a PUT request on the
    /// activity states resource.
    /// </summary>
    public static class PutStateRequest
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
        public static PutStateRequest<T> Create<T>(StateDocument<T> state)
        {
            return new PutStateRequest<T>(state);
        }
    }

    /// <summary>
    /// Class used to perform a PUT request on the activity states resource.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the document to be stored as JSON in the LRS.
    /// The type must support JSON serialization.
    /// </typeparam>
    public class PutStateRequest<T> : ASingleStateRequest
    {
        /// <summary>
        /// The document used for concurrency comparisons.
        /// </summary>
        public StateDocument<T> State { get; }

        internal PutStateRequest(StateDocument<T> state)
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
