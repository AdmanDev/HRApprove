namespace HRApprove.Domain.Exceptions.Bases
{
    /// <summary>
    /// Represents a conflict exception.
    /// </summary>
    public class ConflictException : HRException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ConflictException(string message)
            : base(message)
        {
        }
    }
}
