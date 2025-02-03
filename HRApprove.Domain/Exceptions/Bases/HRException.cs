namespace HRApprove.Domain.Exceptions.Bases
{
    using System;

    /// <summary>
    /// Represents the base exception.
    /// </summary>
    public abstract class HRException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HRException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public HRException(string message)
            : base(message)
        {
        }
    }
}
