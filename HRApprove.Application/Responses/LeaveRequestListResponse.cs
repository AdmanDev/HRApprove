namespace HRApprove.Application.Responses
{
    using HRApprove.Application.DTOs;
    using HRApprove.Application.Responses.Bases;

    /// <summary>
    /// Represents a response for a list of leave requests.
    /// </summary>
    public class LeaveRequestListResponse : SuccessApiResponse<IEnumerable<LeaveRequestDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequestListResponse"/> class.
        /// </summary>
        /// <param name="leaveRequestList">The list of leave requests.</param>
        public LeaveRequestListResponse(IEnumerable<LeaveRequestDto> leaveRequestList)
            : base(leaveRequestList)
        {
        }
    }
}
