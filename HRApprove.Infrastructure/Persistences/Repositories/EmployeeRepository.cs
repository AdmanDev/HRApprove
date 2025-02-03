namespace HRApprove.Infrastructure.Persistences.Repositories
{
    using System.Data;
    using System.Threading.Tasks;
    using Dapper;
    using HRApprove.Domain.Entities;
    using HRApprove.Domain.Interfaces.Repositories;
    using HRApprove.Infrastructure.Exceptions;
    using HRApprove.Infrastructure.Interfaces;

    /// <summary>
    /// Represents a repository for employees.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMySqlConnectionFactory connectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">The database connection factory.</param>
        public EmployeeRepository(IMySqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        /// <inheritdoc />
        public async Task<Employee?> GetByIdAsync(int id)
        {
            try
            {
                var query = @"
                    SELECT e.EmployeeId, e.FirstName, e.LastName, e.Email, e.DateHired FROM Employees e;
                    SELECT r.RoleId, r.Label FROM Roles r
                    JOIN EmployeeRoles er ON r.RoleId = er.RoleId
                    WHERE er.EmployeeId = @EmployeeId;";

                using IDbConnection connection = this.connectionFactory.CreateConnection();

                using SqlMapper.GridReader multi = await connection.QueryMultipleAsync(query, new { EmployeeId = id });

                Employee? employee = await multi.ReadFirstOrDefaultAsync<Employee>();
                if (employee != null)
                {
                    IEnumerable<Role> roles = await multi.ReadAsync<Role>();
                    employee = new Employee(
                        employee.EmployeeId,
                        employee.FirstName,
                        employee.LastName,
                        employee.Email,
                        employee.DateHired,
                        roles);
                }

                return employee;
            }
            catch (Exception e)
            {
                throw new SqlException("An error occurred while retrieving the employee.", e);
            }
        }
    }
}
