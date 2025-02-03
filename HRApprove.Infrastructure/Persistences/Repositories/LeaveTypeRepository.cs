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
    /// Represents a repository for leave types.
    /// </summary>
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly IMySqlConnectionFactory connectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveTypeRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">The database connection factory.</param>
        public LeaveTypeRepository(IMySqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        /// <inheritdoc />
        public async Task<LeaveType?> GetByTypeLabel(string label)
        {
            try
            {
                const string query = @"
                                SELECT LeaveTypeId, Label FROM LeaveTypes 
                                WHERE Label = @TypeName;";

                using IDbConnection connection = this.connectionFactory.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<LeaveType>(query, new { TypeName = label });
            }
            catch (Exception e)
            {
                throw new SqlException($"An SQL error occurred while retrieving leave type : {label}.", e);
            }
        }
    }
}
