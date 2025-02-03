namespace HRApprove.Domain.Entities
{
    using HRApprove.Domain.Exceptions;
    using HRApprove.Domain.ValueObjects;

    /// <summary>
    /// Represents a leave request from an employee.
    /// </summary>
    public class LeaveRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequest"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the leave request.</param>
        /// <param name="employee">The employee  that requested the leave.</param>
        /// <param name="leaveType">The type of leave requested.</param>
        /// <param name="startDate">The start date of the leave.</param>
        /// <param name="endDate">The end date of the leave.</param>
        /// <param name="status">The status of the leave request.</param>
        /// <param name="comment">The comment of the employee regarding the leave request.</param>
        /// <param name="hrComment">The comment of the HR regarding the leave request decision.</param>
        public LeaveRequest(int id, Employee employee, LeaveType leaveType, DateTime startDate, DateTime endDate, LeaveStatus status, string? comment, string? hrComment = null)
        {
            if (startDate >= endDate)
            {
                throw new DateRangeException();
            }

            this.LeaveRequestId = id;
            this.Employee = employee;
            this.LeaveType = leaveType;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Status = status;
            this.Comment = comment;
            this.HRComment = hrComment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequest"/> class.
        /// </summary>
        /// <param name="employee">The employee  that requested the leave.</param>
        /// <param name="leaveType">The type of leave requested.</param>
        /// <param name="startDate">The start date of the leave.</param>
        /// <param name="endDate">The end date of the leave.</param>
        /// <param name="comment">The comment of the employee regarding the leave request.</param>
        public LeaveRequest(Employee employee, LeaveType leaveType, DateTime startDate, DateTime endDate, string? comment = null)
            : this(0, employee, leaveType, startDate, endDate, LeaveStatus.Pending, comment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequest"/> class.
        /// </summary>
        public LeaveRequest()
        {
            this.Employee = new Employee();
            this.LeaveType = new LeaveType();
        }

        /// <summary>
        /// Gets the unique identifier of the leave request.
        /// </summary>
        public int LeaveRequestId { get; private set; }

        /// <summary>
        /// Gets the employee id that requested the leave.
        /// </summary>
        public Employee Employee { get; private set; }

        /// <summary>
        /// Gets the type of leave requested.
        /// </summary>
        public LeaveType LeaveType { get; private set; }

        /// <summary>
        /// Gets the start date of the leave.
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Gets the end date of the leave.
        /// </summary>
        public DateTime EndDate { get; private set; }

        /// <summary>
        /// Gets the comment of the employee regarding the leave request.
        /// </summary>
        public string? Comment { get; private set; }

        /// <summary>
        /// Gets the status of the leave request.
        /// </summary>
        public LeaveStatus Status { get; private set; }

        /// <summary>
        /// Gets the comment of the HR regarding the leave request.
        /// </summary>
        public string? HRComment { get; private set; }

        /// <summary>
        /// Approves or rejects the leave request.
        /// </summary>
        /// <param name="isApproved">A value indicating whether the leave request is approved or rejected.</param>
        /// <param name="hrComment">The comment of the HR regarding the leave request decision.</param>
        public void ProcessApproval(bool isApproved, string? hrComment)
        {
            if (this.Status != LeaveStatus.Pending)
            {
                throw new LeaveRequestAlreadyProcessedException();
            }

            this.Status = isApproved ? LeaveStatus.Approved : LeaveStatus.Rejected;
            this.HRComment = hrComment;
        }
    }
}
