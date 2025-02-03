namespace HRApprove.Domain.Services
{
    using HRApprove.Domain.Entities;
    using HRApprove.Domain.Exceptions.Bases;
    using HRApprove.Domain.Interfaces.Services;

    /// <summary>
    /// Represents a service that handles leave approval logic.
    /// </summary>
    public class LeaveApprovalService : ILeaveApprovalService
    {
        /// <inheritdoc/>
        public void ReviewLeaveRequest(Employee approver, LeaveRequest leaveRequest, bool isApproved, string? hrComment)
        {
            if (!approver.HasRole("HR"))
            {
                throw new UnauthorizedException("Only HR managers can approve or reject leave requests.");
            }

            if (approver.EmployeeId == leaveRequest.Employee.EmployeeId)
            {
                throw new BadRequestException("You cannot approve or reject your own leave request.");
            }

            leaveRequest.ProcessApproval(isApproved, hrComment);
        }
    }
}
