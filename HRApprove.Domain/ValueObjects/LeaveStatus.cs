namespace HRApprove.Domain.ValueObjects
{
    /// <summary>
    /// Represents the status of the leave request.
    /// </summary>
    public enum LeaveStatus
    {
        /// <summary>
        /// Represents the pending status.
        /// </summary>
        Pending,

        /// <summary>
        /// Represents the approved status.
        /// </summary>
        Approved,

        /// <summary>
        /// Represents the rejected status.
        /// </summary>
        Rejected,
    }
}
