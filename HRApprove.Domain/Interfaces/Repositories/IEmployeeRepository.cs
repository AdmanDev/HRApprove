namespace HRApprove.Domain.Interfaces.Repositories
{
    using System.Threading.Tasks;
    using HRApprove.Domain.Entities;

    /// <summary>
    /// Represents the interface for repositories that handle employees data.
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The employee object if found; otherwise, null.</returns>
        Task<Employee?> GetByIdAsync(int id);
    }
}
