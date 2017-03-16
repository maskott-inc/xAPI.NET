using System;
using System.Collections.Generic;
using xAPI.Client.Resources;
using xAPI.Client.Validation;

namespace xAPI.Client.Requests
{
    public class PostStatementsRequest : ARequest
    {
        public IReadOnlyList<Statement> Statements { get; set; }

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
