namespace HRApprove.Application.Queries.LeaveRequest.GetAll
{
    using HRApprove.Application.DTOs;
    using MediatR;

    /// <summary>
    /// Represents the query to get all leave requests.
    /// </summary>
    public class GetAllLeaveRequestsQuery : IRequest<IEnumerable<LeaveRequestDto>>
    {
    }
}
