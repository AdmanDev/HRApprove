namespace HRApprove.Domain.Entities
{
    /// <summary>
    /// Represents a leave request type.
    /// </summary>
    public class LeaveType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveType"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the leave type.</param>
        /// <param name="label">The type label.</param>
        public LeaveType(int id, string label)
        {
            this.LeaveTypeId = id;
            this.Label = label;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveType"/> class.
        /// </summary>
        public LeaveType()
            : this(0, string.Empty)
        {
        }

        /// <summary>
        /// Gets the unique identifier of the leave type.
        /// </summary>
        public int LeaveTypeId { get; private set; }

        /// <summary>
        /// Gets the name of the leave type.
        /// </summary>
        public string Label { get; private set; }
    }
}
