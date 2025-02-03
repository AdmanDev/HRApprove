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
    /// Represents a repository for leave requests.
    /// </summary>
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly IMySqlConnectionFactory connectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequestRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">The database connection factory.</param>
        public LeaveRequestRepository(IMySqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<LeaveRequest>> GetAllAsync()
        {
            try
            {
                const string query = @"
                SELECT 
                    lr.LeaveRequestId,
                    lr.StartDate,
                    lr.EndDate,
                    lr.Comment,
                    lr.Status,
                    lr.HRComment,
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.Email,
                    e.DateHired,
                    lt.LeaveTypeId,
                    lt.Label
                FROM LeaveRequests lr
                INNER JOIN Employees e ON lr.EmployeeId = e.EmployeeId
                INNER JOIN LeaveTypes lt ON lr.LeaveTypeId = lt.LeaveTypeId";

                using IDbConnection connection = this.connectionFactory.CreateConnection();
                IEnumerable<LeaveRequest> result = await this.QueryLeaveRequestsAsync(query);

                return result;
            }
            catch (Exception e)
            {
                throw new SqlException("An error occurred while retrieving the leave requests.", e);
            }
        }

        /// <inheritdoc />
        public async Task<LeaveRequest?> GetByIdAsync(int id)
        {
            try
            {
                const string query = @"
                SELECT 
                    lr.LeaveRequestId,
                    lr.StartDate,
                    lr.EndDate,
                    lr.Comment,
                    lr.Status,
                    lr.HRComment,
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.Email,
                    e.DateHired,
                    lt.LeaveTypeId,
                    lt.Label
                FROM LeaveRequests lr
                INNER JOIN Employees e ON lr.EmployeeId = e.EmployeeId
                INNER JOIN LeaveTypes lt ON lr.LeaveTypeId = lt.LeaveTypeId
                WHERE lr.LeaveRequestId = @Id";

                IEnumerable<LeaveRequest> result = await this.QueryLeaveRequestsAsync(query, new { Id = id });

                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new SqlException("An error occurred while retrieving the leave request.", e);
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(int employeeId)
        {
            try
            {
                const string query = @"
                SELECT 
                    lr.LeaveRequestId,
                    lr.StartDate,
                    lr.EndDate,
                    lr.Comment,
                    lr.Status,
                    lr.HRComment,
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.Email,
                    e.DateHired,
                    lt.LeaveTypeId,
                    lt.Label
                FROM LeaveRequests lr
                INNER JOIN Employees e ON lr.EmployeeId = e.EmployeeId
                INNER JOIN LeaveTypes lt ON lr.LeaveTypeId = lt.LeaveTypeId
                WHERE lr.EmployeeId = @Id";

                IEnumerable<LeaveRequest> result = await this.QueryLeaveRequestsAsync(query, new { Id = employeeId });

                return result;
            }
            catch (Exception e)
            {
                throw new SqlException("An error occurred while retrieving the leave request.", e);
            }
        }

        /// <inheritdoc />
        public async Task AddAsync(LeaveRequest leaveRequest)
        {
            try
            {
                const string query = @"
                        INSERT INTO LeaveRequests (EmployeeId, LeaveTypeId, StartDate, EndDate, Comment, Status) 
                        VALUES (@EmployeeId, @LeaveTypeId, @StartDate, @EndDate, @Comment, @Status);";

                using IDbConnection connection = this.connectionFactory.CreateConnection();
                await connection.ExecuteAsync(query, new
                {
                    leaveRequest.Employee.EmployeeId,
                    leaveRequest.LeaveType.LeaveTypeId,
                    leaveRequest.StartDate,
                    leaveRequest.EndDate,
                    leaveRequest.Comment,
                    Status = leaveRequest.Status.ToString(),
                });
            }
            catch (Exception e)
            {
                throw new SqlException("An error occurred while adding the leave request.", e);
            }
        }

        /// <inheritdoc />
        public async Task UpdateAsync(LeaveRequest leaveRequest)
        {
            try
            {
                const string query = @"
                        UPDATE LeaveRequests 
                        SET LeaveTypeId = @LeaveTypeId,
                            StartDate = @StartDate,
                            EndDate = @EndDate,
                            Comment = @Comment,
                            Status = @Status,
                            HRComment = @HRComment
                        WHERE LeaveRequestId = @LeaveRequestId;";

                using IDbConnection connection = this.connectionFactory.CreateConnection();
                await connection.ExecuteAsync(query, new
                {
                    leaveRequest.LeaveRequestId,
                    leaveRequest.LeaveType.LeaveTypeId,
                    leaveRequest.StartDate,
                    leaveRequest.EndDate,
                    leaveRequest.Comment,
                    leaveRequest.HRComment,
                    Status = leaveRequest.Status.ToString(),
                });
            }
            catch (Exception e)
            {
                throw new SqlException("An error occurred while updating the leave request.", e);
            }
        }

        private async Task<IEnumerable<LeaveRequest>> QueryLeaveRequestsAsync(string query, object? param = null)
        {
            using IDbConnection connection = this.connectionFactory.CreateConnection();

            return await connection.QueryAsync<LeaveRequest, Employee, LeaveType, LeaveRequest>(
                query,
                (leaveRequest, employee, leaveType) =>
                {
                    return new LeaveRequest(
                        leaveRequest.LeaveRequestId,
                        employee,
                        leaveType,
                        leaveRequest.StartDate,
                        leaveRequest.EndDate,
                        leaveRequest.Status,
                        leaveRequest.Comment,
                        leaveRequest.HRComment);
                },
                param,
                splitOn: "EmployeeId, LeaveTypeId");
        }
    }
}
