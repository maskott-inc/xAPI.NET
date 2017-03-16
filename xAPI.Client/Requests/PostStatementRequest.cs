﻿using System;
using xAPI.Client.Resources;
using xAPI.Client.Validation;

namespace xAPI.Client.Requests
{
    public class PostStatementRequest : ARequest
    {
        public Statement Statement { get; set; }

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
