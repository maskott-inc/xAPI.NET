using System;
using System.Collections.Generic;
using xAPI.Client.Resources;
using xAPI.Client.Validation;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Class used to POST multiple statements to the LRS.
    /// </summary>
    public class PostStatementsRequest : ARequest
    {
        internal IReadOnlyList<Statement> Statements { get; set; }

        /// <summary>
        /// Creates a new instance of PostStatementsRequest.
        /// </summary>
        /// <param name="statements">The statements to POST.</param>
        public PostStatementsRequest(IReadOnlyList<Statement> statements)
        {
            this.Statements = statements;
        }

        internal override void Validate()
        {
            if (this.Statements == null)
            {
                throw new ArgumentNullException(nameof(this.Statements));
            }

            Validator.Validate(this.Statements);
        }
    }
}
