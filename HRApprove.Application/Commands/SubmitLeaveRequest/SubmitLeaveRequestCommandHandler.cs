namespace HRApprove.Application.Commands.SubmitLeaveRequest
{
    using System.Threading;
    using System.Threading.Tasks;
    using HRApprove.Application.DTOs;
    using HRApprove.Domain.Entities;
    using HRApprove.Domain.Exceptions.Bases;
    using HRApprove.Domain.Interfaces.Repositories;
    using MediatR;

    /// <summary>
    /// Represents the command handler for submitting a leave request.
    /// </summary>
    public class SubmitLeaveRequestCommandHandler : IRequestHandler<SubmitLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IEmployeeRepository employeeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitLeaveRequestCommandHandler"/> class.
        /// </summary>
        /// <param name="leaveRequestRepository">the repository for leave requests.</param>
        /// <param name="leaveTypeRepository">The leave type repository.</param>
        /// <param name="employeeRepository">The employee repository.</param>
        public SubmitLeaveRequestCommandHandler(
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IEmployeeRepository employeeRepository)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Handles the command for submitting a leave request.
        /// </summary>
        /// <param name="request">The command to submit a leave request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An async task.</returns>
        public async Task Handle(SubmitLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            CreateLeaveRequestDto leaveRequestDto = request.LeaveRequest;

            Employee? employee = await this.employeeRepository.GetByIdAsync(leaveRequestDto.EmployeeId);
            if (employee == null)
            {
                throw new NotFoundException("The employee who is requesting the leave does not exist.");
            }

            LeaveType? leaveType = await this.leaveTypeRepository.GetByTypeLabel(leaveRequestDto.Type);
            if (leaveType == null)
            {
                throw new BadRequestException($"The leave type {leaveRequestDto.Type} is invalid.");
            }

            LeaveRequest leaveRequest = new LeaveRequest(
                employee, leaveType, leaveRequestDto.StartDate, leaveRequestDto.EndDate, leaveRequestDto.Comment);

            await this.leaveRequestRepository.AddAsync(leaveRequest);
        }
    }
}
