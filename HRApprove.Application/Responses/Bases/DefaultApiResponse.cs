namespace HRApprove.Application.Responses.Bases
{
    /// <summary>
    /// Represents the default response model of an http request.
    /// </summary>
    public class DefaultApiResponse : BaseApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultApiResponse"/> class.
        /// </summary>
        public DefaultApiResponse()
            : base(true)
        {
        }
    }
}
