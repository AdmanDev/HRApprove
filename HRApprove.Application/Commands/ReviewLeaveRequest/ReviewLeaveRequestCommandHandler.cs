namespace HRApprove.Application.Commands.ReviewLeaveRequest
{
    using HRApprove.Domain.Entities;
    using HRApprove.Domain.Exceptions.Bases;
    using HRApprove.Domain.Interfaces.Repositories;
    using HRApprove.Domain.Interfaces.Services;
    using MediatR;

    /// <summary>
    /// Represents a command handler for reviewing a leave request.
    /// </summary>
    public class ReviewLeaveRequestCommandHandler : IRequestHandler<ReviewLeaveRequestCommand>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveApprovalService leaveApprovalService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewLeaveRequestCommandHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="leaveRequestRepository">The leave request repository.</param>
        /// <param name="leaveApprovalService">The leave approval service.</param>
        public ReviewLeaveRequestCommandHandler(
            IEmployeeRepository employeeRepository,
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveApprovalService leaveApprovalService)
        {
            this.employeeRepository = employeeRepository;
            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveApprovalService = leaveApprovalService;
        }

        /// <summary>
        /// Handles the review leave request command.
        /// </summary>
        /// <param name="request">The review leave request command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An async task.</returns>
        public async Task Handle(ReviewLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            Employee? employee = await this.employeeRepository.GetByIdAsync(request.ApproverId);
            if (employee == null)
            {
                throw new UnauthorizedException("Access denied.");
            }

            LeaveRequest? leaveRequest = await this.leaveRequestRepository.GetByIdAsync(request.LeaveRequestId);
            if (leaveRequest == null)
            {
                throw new NotFoundException("Leave request not found.");
            }

            this.leaveApprovalService.ReviewLeaveRequest(employee, leaveRequest, request.IsApproved, request.Reason);

            await this.leaveRequestRepository.UpdateAsync(leaveRequest);
        }
    }
}
