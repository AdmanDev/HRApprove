namespace HRApprove.Domain.Exceptions
{
    using HRApprove.Domain.Exceptions.Bases;

    /// <summary>
    /// Represents an exception thrown when the start date is greater than the end date.
    /// </summary>
    public class DateRangeException : BadRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateRangeException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public DateRangeException()
            : base("The start date cannot be greater than the end date.")
        {
        }
    }
}
