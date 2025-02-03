namespace HRApprove.Application.Extensions
{
    using HRApprove.Application.DTOs;
    using HRApprove.Domain.Entities;

    /// <summary>
    /// Represent the extensions to map entities to dtos.
    /// </summary>
    public static class DtoMappingExtensions
    {
        /// <summary>
        /// Converts a leave request entity to its corresponding dto.
        /// </summary>
        /// <param name="leaveRequest">The leave request entity to convert.</param>
        /// <returns>The leave request dto.</returns>
        public static LeaveRequestDto ToDto(this LeaveRequest leaveRequest)
        {
            return new LeaveRequestDto(
                leaveRequest.LeaveRequestId,
                leaveRequest.Employee.ToDto(),
                leaveRequest.LeaveType.Label,
                leaveRequest.StartDate,
                leaveRequest.EndDate,
                leaveRequest.Comment,
                leaveRequest.HRComment);
        }

        /// <summary>
        /// Converts an employee entity to its corresponding dto.
        /// </summary>
        /// <param name="employee">The employee entity to convert.</param>
        /// <returns>The employee dto.</returns>
        public static EmployeeDto ToDto(this Employee employee)
        {
            return new EmployeeDto(
                employee.EmployeeId,
                employee.FirstName,
                employee.LastName,
                employee.DateHired);
        }
    }
}
