using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace xAPI.Client.Exceptions
{
    /// <summary>
    /// This exception is thrown when an invalid request is performed by the application.
    /// Details are included in the exception's members.
    /// </summary>
    public class ValidationException : XApiException
    {
        /// <summary>
        /// The object that caused the exception to be thrown.
        /// </summary>
        public object ObjectToValidate { get; private set; }

        /// <summary>
        /// A list of validation errors related to the object.
        /// </summary>
        public IReadOnlyList<ValidationError> ValidationErrors { get; private set; }

        /// <summary>
        /// Creates a new instance of HttpException.
        /// </summary>
        /// <param name="objectToValidate">The object that caused the exception to be thrown.</param>
        /// <param name="validationErrors">A list of validation errors related to the object.</param>
        public ValidationException(object objectToValidate, List<ValidationError> validationErrors)
            : base($"Failed to validate object of type {objectToValidate.GetType().FullName}. Validation errors: {JsonConvert.SerializeObject(validationErrors)}")
        {
            this.ObjectToValidate = objectToValidate;
            this.ValidationErrors = validationErrors;
        }
    }

    /// <summary>
    /// Represents a validation error, i.e. an association between one or more class
    /// members and an error message. Also contains child validation errors (if the
    /// member is alone and contains sub-members that failed to validate).
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// The members that failed to validate.
        /// </summary>
        [JsonProperty("members")]
        public IReadOnlyList<string> MemberNames { get; private set; }

        /// <summary>
        /// The error message associated with the validation failure.
        /// </summary>
        [JsonProperty("error_message")]
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// The children validation errors, if the member is alone and contains
        /// sub-members that failed to validate.
        /// </summary>
        [JsonProperty("inner_errors", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IReadOnlyList<ValidationError> InnerErrors { get; private set; }

        /// <summary>
        /// Creates a new instance of ValidationError.
        /// </summary>
        /// <param name="memberNames">The members that failed to validate.</param>
        /// <param name="errorMessage">The error message associated with the validation failure.</param>
        /// <param name="innerErrors">The children validation errors.</param>
        public ValidationError(IEnumerable<string> memberNames, string errorMessage, IEnumerable<ValidationError> innerErrors)
        {
            this.MemberNames = memberNames.ToList();
            this.ErrorMessage = errorMessage;
            this.InnerErrors = innerErrors?.ToList();
        }
    }
}
