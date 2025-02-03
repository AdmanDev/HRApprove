namespace HRApprove.Application.Exceptions
{
    using HRApprove.Domain.Exceptions.Bases;

    /// <summary>
    /// Represents a data validation exception.
    /// </summary>
    public class DataValidationException : BadRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataValidationException"/> class.
        /// </summary>
        /// <param name="errors">The validation errors.</param>
        public DataValidationException(Dictionary<string, string[]> errors)
        : base("Validation errors occurred")
        {
            this.Errors = errors;
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        public Dictionary<string, string[]> Errors { get; }
    }
}
