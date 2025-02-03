namespace HRApprove.Application.Responses.Bases
{
    /// <summary>
    /// Represents the base response model of an http request.
    /// </summary>
    public abstract class BaseApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiResponse"/> class.
        /// </summary>
        /// <param name="isSuccess">A value indicating whether the request was successful.</param>
        public BaseApiResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        /// <summary>
        /// Gets a value indicating whether the request was successful.
        /// </summary>
        public bool IsSuccess { get; }
    }
}
