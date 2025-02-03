namespace HRApprove.Domain.Exceptions.Bases
{
    /// <summary>
    /// Represents a not found exception.
    /// </summary>
    public class NotFoundException : HRException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
