using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace xAPI.Client.Validation
{
    internal class CompositeValidationResult : ValidationResult
    {
        private readonly IReadOnlyList<ValidationResult> _innerResults;

        public IReadOnlyList<ValidationResult> InnerResults
        {
            get
            {
                return this._innerResults;
            }
        }

        public CompositeValidationResult(string errorMessage, string memberName, List<ValidationResult> innerResults)
            : base(errorMessage, new string[] { memberName })
        {
            this._innerResults = innerResults;
        }
    }
}
