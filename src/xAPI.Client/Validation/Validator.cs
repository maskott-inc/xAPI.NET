using System.Collections.Generic;
using System.Linq;
using xAPI.Client.Exceptions;
using DA = System.ComponentModel.DataAnnotations;

namespace xAPI.Client.Validation
{
    internal static class Validator
    {
        public static void Validate(object objectToValidate)
        {
            var context = new DA.ValidationContext(objectToValidate);
            var validationResults = new List<DA.ValidationResult>();

            bool isValid = DA.Validator.TryValidateObject(objectToValidate, context, validationResults, validateAllProperties: true);
            if (!isValid)
            {
                var errors = validationResults
                    .Select(x => new ValidationError(x.MemberNames, x.ErrorMessage, GetInnerErrors(x)))
                    .ToList();
                throw new ValidationException(objectToValidate, errors);
            }
        }

        private static IEnumerable<ValidationError> GetInnerErrors(DA.ValidationResult validationResult)
        {
            if (validationResult is CompositeValidationResult compositeValidationResult)
            {
                return compositeValidationResult.InnerResults
                    .Select(x => new ValidationError(x.MemberNames, x.ErrorMessage, GetInnerErrors(x)))
                    .ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
