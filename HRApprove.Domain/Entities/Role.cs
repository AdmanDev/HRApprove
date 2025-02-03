namespace HRApprove.Domain.Entities
{
    /// <summary>
    /// Represents a role assigned to an employee.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <param name="label">The role label.</param>
        public Role(int roleId, string label)
        {
            this.RoleId = roleId;
            this.Label = label;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
            : this(0, string.Empty)
        {
        }

        /// <summary>
        /// Gets the unique identifier of the role.
        /// </summary>
        public int RoleId { get; private set; }

        /// <summary>
        /// Gets the label of the role.
        /// </summary>
        public string Label { get; private set; }
    }
}