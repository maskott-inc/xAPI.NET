using System;
using System.Collections;
using System.Collections.Generic;
using DA = System.ComponentModel.DataAnnotations;

namespace xAPI.Client.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ValidatePropertyAttribute : DA.ValidationAttribute
    {
        protected override DA.ValidationResult IsValid(object value, DA.ValidationContext validationContext)
        {
            if (value == null)
            {
                return DA.ValidationResult.Success;
            }

            var results = new List<DA.ValidationResult>();

            if (value is IEnumerable)
            {
                // value is an enumerable, so we have to validate each element
                // individually.

                var enumerable = value as IEnumerable;
                int index = 0;
                foreach (var val in enumerable)
                {
                    if (val != null)
                    {
                        // Validate the list element and wrap any validation info
                        // inside a composite result

                        var tmpResults = new List<DA.ValidationResult>();
                        var tmpContext = new DA.ValidationContext(val);
                        DA.Validator.TryValidateObject(val, tmpContext, tmpResults, true);

                        if (tmpResults.Count > 0)
                        {
                            var result = new CompositeValidationResult("Failed to validate property.", $"{validationContext.DisplayName}#{index}", tmpResults);
                            results.Add(result);
                        }
                    }
                    else
                    {
                        // Cannot validate a null element in a list

                        var result = new DA.ValidationResult("Element is null.", new string[] { $"{validationContext.DisplayName}#{index}" });
                        results.Add(result);
                    }

                    index++;
                }
            }
            else
            {
                // Validate value normally and keep trace of the sub-results

                var context = new DA.ValidationContext(value);
                DA.Validator.TryValidateObject(value, context, results, true);
            }

            // Wrap all sub results inside a composite result to be able to gather
            // useful information
            if (results.Count > 0)
            {
                return new CompositeValidationResult($"Failed to validate property.", validationContext.DisplayName, results);
            }
            else
            {
                return DA.ValidationResult.Success;
            }
        }
    }
}
