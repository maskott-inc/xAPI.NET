using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace xAPI.Client.Exceptions
{
    public class ValidationException : XApiException
    {
        public object ObjectToValidate { get; private set; }
        public IReadOnlyList<ValidationError> ValidationErrors { get; private set; }

        public ValidationException(object objectToValidate, List<ValidationError> validationErrors)
            : base($"Failed to validate object of type {objectToValidate.GetType().FullName}. Validation errors: {JsonConvert.SerializeObject(validationErrors)}")
        {
            this.ObjectToValidate = objectToValidate;
            this.ValidationErrors = validationErrors;
        }
    }

    public class ValidationError
    {
        [JsonProperty("members")]
        public IReadOnlyList<string> MemberNames { get; private set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; private set; }

        [JsonProperty("inner_errors", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IReadOnlyList<ValidationError> InnerErrors { get; private set; }

        public ValidationError(IEnumerable<string> memberNames, string errorMessage, IEnumerable<ValidationError> innerErrors)
        {
            this.MemberNames = memberNames.ToList();
            this.ErrorMessage = errorMessage;
            this.InnerErrors = innerErrors?.ToList();
        }
    }
}
