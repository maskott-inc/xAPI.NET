using System;
using xAPI.Client.Resources;
using xAPI.Client.Validation;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Class used to PUT a single statement from the LRS.
    /// </summary>
    public class PutStatementRequest : ARequest
    {
        internal Statement Statement { get; set; }

        /// <summary>
        /// Creates a new instance of PutStatementRequest.
        /// </summary>
        /// <param name="statement">The statement to PUT.</param>
        public PutStatementRequest(Statement statement)
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
