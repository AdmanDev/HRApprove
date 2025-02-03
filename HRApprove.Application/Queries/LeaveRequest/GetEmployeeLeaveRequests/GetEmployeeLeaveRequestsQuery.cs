namespace HRApprove.Application.Queries.LeaveRequest.GetEmployeeLeaveRequests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HRApprove.Application.DTOs;
    using MediatR;

    /// <summary>
    /// Represents the get employee leave requests query.
    /// </summary>
    public class GetEmployeeLeaveRequestsQuery : IRequest<IEnumerable<LeaveRequestDto>>
    {
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        [Required(ErrorMessage = "The employee id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The employee id is invalid")]
        public int EmployeeId { get; set; }
    }
}
