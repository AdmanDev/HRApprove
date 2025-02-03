namespace HRApprove.Domain.Entities
{
    /// <summary>
    /// Represents the employee entity.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="firstName">The employee first name.</param>
        /// <param name="lastName">The employee last name.</param>
        /// <param name="email">The employee email.</param>
        /// <param name="dateHired">The hire date.</param>
        /// <param name="roles">The employee roles.</param>
        public Employee(int employeeId, string firstName, string lastName, string email, DateTime dateHired, IEnumerable<Role> roles)
        {
            this.EmployeeId = employeeId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.DateHired = dateHired;
            this.Roles = roles;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        public Employee()
        {
            this.Roles = new List<Role>();
        }

        /// <summary>
        /// Gets the employee id.
        /// </summary>
        public int EmployeeId { get; private set; }

        /// <summary>
        /// Gets the employee first name.
        /// </summary>
        public string FirstName { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the employee last name.
        /// </summary>
        public string LastName { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the employee email.
        /// </summary>
        public string Email { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the hire date.
        /// </summary>
        public DateTime DateHired { get; private set; }

        /// <summary>
        /// Gets the employee roles.
        /// </summary>
        public IEnumerable<Role> Roles { get; private set; }

        /// <summary>
        /// Checks if the employee has the specified role.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <returns>A value indicating whether the employee has the specified role.</returns>
        public bool HasRole(string roleName)
        {
            return this.Roles.Any(r => r.Label == roleName);
        }
    }
}
