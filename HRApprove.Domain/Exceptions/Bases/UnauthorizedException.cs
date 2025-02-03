namespace HRApprove.Domain.Exceptions.Bases
{
    /// <summary>
    /// Represents an unauthorized access exception.
    /// </summary>
    public class UnauthorizedException : HRException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public UnauthorizedException(string message)
            : base(message)
        {
        }
    }
}
