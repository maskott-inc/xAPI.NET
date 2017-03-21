using System;
using xAPI.Client.Resources;
using xAPI.Client.Validation;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Class used to POST a single statement from the LRS.
    /// </summary>
    public class PostStatementRequest : ARequest
    {
        internal Statement Statement { get; set; }

        /// <summary>
        /// Creates a new instance of PostStatementRequest.
        /// </summary>
        /// <param name="statement">The statement to POST.</param>
        public PostStatementRequest(Statement statement)
        {
            this.Statement = statement;
        }

        internal override void Validate()
        {
            if (this.Statement == null)
            {
                throw new ArgumentNullException(nameof(this.Statement));
            }

            Validator.Validate(this.Statement);
        }
    }
}
