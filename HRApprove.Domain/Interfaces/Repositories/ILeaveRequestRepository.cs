namespace HRApprove.Domain.Interfaces.Repositories
{
    using HRApprove.Domain.Entities;

    /// <summary>
    /// Represents the interface for repositories that handle leave requests.
    /// </summary>
    public interface ILeaveRequestRepository
    {
        /// <summary>
        /// Gets all leave requests from the repository.
        /// </summary>
        /// <returns>A list of leave requests.</returns>
        Task<IEnumerable<LeaveRequest>> GetAllAsync();

        /// <summary>
        /// Gets a leave request by its ID.
        /// </summary>
        /// <param name="id">The ID of the leave request.</param>
        /// <returns>The leave request.</returns>
        Task<LeaveRequest?> GetByIdAsync(int id);

        /// <summary>
        /// Gets leave requests of a specific employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>The leave requests.</returns>
        Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(int employeeId);

        /// <summary>
        /// Adds a leave request to the repository.
        /// </summary>
        /// <param name="leaveRequest">The leave request to add.</param>
        /// <returns>Async task.</returns>
        Task AddAsync(LeaveRequest leaveRequest);

        /// <summary>
        /// Updates a leave request in the repository.
        /// </summary>
        /// <param name="leaveRequest">The leave request to update.</param>
        /// <returns>Async task.</returns>
        Task UpdateAsync(LeaveRequest leaveRequest);
    }
}
