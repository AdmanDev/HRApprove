namespace HRApprove.Domain.Exceptions.Bases
{
    using System;

    /// <summary>
    /// Represents a bad request exception.
    /// </summary>
    public class BadRequestException : HRException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
