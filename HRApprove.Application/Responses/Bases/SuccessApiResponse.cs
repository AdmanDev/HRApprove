namespace HRApprove.Application.Responses.Bases
{
    /// <summary>
    /// Represents a successful response of an http request.
    /// </summary>
    /// <typeparam name="TData">The type of the data to send.</typeparam>
    public class SuccessApiResponse<TData> : BaseApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessApiResponse{TData}"/> class.
        /// </summary>
        /// <param name="data">The data of the response.</param>
        public SuccessApiResponse(TData data)
            : base(true)
        {
            this.Data = data;
        }

        /// <summary>
        /// Gets or sets the data of the response.
        /// </summary>
        public TData Data { get; set; }
    }
}
