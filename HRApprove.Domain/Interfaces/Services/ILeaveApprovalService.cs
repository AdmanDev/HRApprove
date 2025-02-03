namespace HRApprove.Domain.Interfaces.Services
{
    using HRApprove.Domain.Entities;

    /// <summary>
    /// Represents an interface for leave approval logic services.
    /// </summary>
    public interface ILeaveApprovalService
    {
        /// <summary>
        /// Approves or rejects a leave request.
        /// </summary>
        /// <param name="approver">The employee who reviews the leave request.</param>
        /// <param name="leaveRequest">The leave request to be reviewed.</param>
        /// <param name="isApproved">A value indicating whether the leave request is approved.</param>
        /// <param name="hrComment">The comment provided by the HR manager.</param>
        void ReviewLeaveRequest(Employee approver, LeaveRequest leaveRequest, bool isApproved, string? hrComment);
    }
}
