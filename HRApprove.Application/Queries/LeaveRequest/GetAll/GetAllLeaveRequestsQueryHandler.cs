namespace HRApprove.Application.Queries.LeaveRequest.GetAll
{
    using System.Threading;
    using System.Threading.Tasks;
    using HRApprove.Application.DTOs;
    using HRApprove.Application.Extensions;
    using HRApprove.Domain.Entities;
    using HRApprove.Domain.Interfaces.Repositories;
    using MediatR;

    /// <summary>
    /// Represents the handler for getting all leave requests.
    /// </summary>
    public class GetAllLeaveRequestsQueryHandler : IRequestHandler<GetAllLeaveRequestsQuery, IEnumerable<LeaveRequestDto>>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllLeaveRequestsQueryHandler"/> class.
        /// </summary>
        /// <param name="leaveRequestRepository">The leave request repository.</param>
        public GetAllLeaveRequestsQueryHandler(ILeaveRequestRepository leaveRequestRepository)
        {
            this.leaveRequestRepository = leaveRequestRepository;
        }

        /// <summary>
        /// Handles the request to get all leave requests.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of leave requests.</returns>
        public async Task<IEnumerable<LeaveRequestDto>> Handle(GetAllLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<LeaveRequest> leaveRequests = await this.leaveRequestRepository.GetAllAsync();

            return leaveRequests.Select(lr => lr.ToDto());
        }
    }
}
