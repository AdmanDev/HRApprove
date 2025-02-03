namespace HRApprove.Application.DTOs
{
    /// <summary>
    /// Represents a data transfer object for submitting a leave request.
    /// </summary>
    public class LeaveRequestDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequestDto"/> class.
        /// </summary>
        /// <param name="leaveRequestId">The ID of the leave request.</param>
        /// <param name="employee">The employee who has submitted the leave request.</param>
        /// <param name="type">The type of leave requested.</param>
        /// <param name="startDate">The start date of the leave request.</param>
        /// <param name="endDate">The end date of the leave request.</param>
        /// <param name="comment">A comment for the leave request.</param>
        /// <param name="hrComment">The HR's comment for the leave request.</param>
        public LeaveRequestDto(int leaveRequestId, EmployeeDto employee, string type, DateTime startDate, DateTime endDate, string? comment = null, string? hrComment = null)
        {
            this.LeaveRequestId = leaveRequestId;
            this.Employee = employee;
            this.LeaveType = type;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Comment = comment;
            this.HRComment = hrComment;
        }

        /// <summary>
        /// Gets or sets the ID of  the leave request.
        /// </summary>
        public int LeaveRequestId { get; set; }

        /// <summary>
        /// Gets or sets the employee who has submitted the leave request.
        /// </summary>
        public EmployeeDto Employee { get; set; }

        /// <summary>
        /// Gets or sets the type of leave requested.
        /// </summary>
        public string LeaveType { get; set; }

        /// <summary>
        /// Gets or sets the start date of the leave request.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the leave request.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets a comment for the leave request.
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Gets or sets the HR's comment for the leave request.
        /// </summary>
        public string? HRComment { get; set; }
    }
}
