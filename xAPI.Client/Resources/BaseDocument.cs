using System;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// Represents an abstract document.
    /// </summary>
    /// <typeparam name="T">The document's type. Must be JSON serializable.</typeparam>
    public abstract class BaseDocument<T>
    {
        /// <summary>
        /// The ETag associated with this document. Always set by the LRS, do not attempt
        /// to modify this property other than for testing purposes.
        /// </summary>
        public string ETag { get; set; }

        /// <summary>
        /// The date the document was last modified. Set by the LRS.
        /// </summary>
        public DateTimeOffset? LastModified { get; set; }

        /// <summary>
        /// The document's content.
        /// </summary>
        public T Content { get; set; }
    }
}
