namespace HRApprove.Domain.Interfaces.Repositories
{
    using HRApprove.Domain.Entities;

    /// <summary>
    /// Represents the interface for repositories that handle leave types.
    /// </summary>
    public interface ILeaveTypeRepository
    {
        /// <summary>
        /// Gets a leave type by its name.
        /// </summary>
        /// <param name="label">The label of leave type to get.</param>
        /// <returns>The leave type if found; otherwise, null.</returns>
        Task<LeaveType?> GetByTypeLabel(string label);
    }
}
