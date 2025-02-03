namespace HRApprove.Application.Responses.Bases
{
    /// <summary>
    /// Represents an error response of an http request.
    /// </summary>
    public class ErrorApiResponse : BaseApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorApiResponse"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ErrorApiResponse(string message)
            : base(false)
        {
            this.ErrorMessage = message;
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
