namespace HRApprove.Application.DTOs
{
    /// <summary>
    /// Represents a data transfer object for an employee.
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeDto"/> class.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="firstName">The employee first name.</param>
        /// <param name="lastName">The employee last name.</param>
        /// <param name="dateHired">The employee hire date.</param>
        public EmployeeDto(int employeeId, string firstName, string lastName, DateTime dateHired)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            DateHired = dateHired;
        }

        /// <summary>
        /// Gets or sets the employee id.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the employee first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the employee last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hire date.
        /// </summary>
        public DateTime DateHired { get; set; }
    }
}
