namespace HRApprove.Application.Queries.LeaveRequest.GetEmployeeLeaveRequests
{
    using HRApprove.Application.DTOs;
    using HRApprove.Application.Extensions;
    using HRApprove.Domain.Entities;
    using HRApprove.Domain.Interfaces.Repositories;
    using MediatR;

    /// <summary>
    /// Represents the handler for getting employee leave requests.
    /// </summary>
    public class GetEmployeeLeaveRequestsQueryHandler : IRequestHandler<GetEmployeeLeaveRequestsQuery, IEnumerable<LeaveRequestDto>>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmployeeLeaveRequestsQueryHandler"/> class.
        /// </summary>
        /// <param name="leaveRequestRepository">The leave request repository.</param>
        public GetEmployeeLeaveRequestsQueryHandler(ILeaveRequestRepository leaveRequestRepository)
        {
            this.leaveRequestRepository = leaveRequestRepository;
        }

        /// <summary>
        /// Handles the request to get employee leave requests.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of leave requests of the employee.</returns>
        public async Task<IEnumerable<LeaveRequestDto>> Handle(GetEmployeeLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<LeaveRequest> leaveRequests = await this.leaveRequestRepository.GetByEmployeeIdAsync(request.EmployeeId);

            return leaveRequests.Select(lr => lr.ToDto());
        }
    }
}
