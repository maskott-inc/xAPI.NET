using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace xAPI.Client.Validation
{
    internal class CompositeValidationResult : ValidationResult
    {
        public IReadOnlyList<ValidationResult> InnerResults { get; private set; }

        public CompositeValidationResult(string errorMessage, string memberName, List<ValidationResult> innerResults)
            : base(errorMessage, new string[] { memberName })
        {
            this.InnerResults = innerResults;
        }
    }
}
