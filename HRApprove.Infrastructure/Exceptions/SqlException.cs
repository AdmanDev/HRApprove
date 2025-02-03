namespace HRApprove.Infrastructure.Exceptions
{
    using System;

    /// <summary>
    /// Represents a SQL exception.
    /// </summary>
    public class SqlException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SqlException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
