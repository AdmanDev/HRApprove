namespace HRApprove.Domain.Exceptions
{
    using HRApprove.Domain.Exceptions.Bases;

    /// <summary>
    /// Represents an exception thrown when a leave request has already been approved or rejected.
    /// </summary>
    public class LeaveRequestAlreadyProcessedException : ConflictException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequestAlreadyProcessedException"/> class.
        /// </summary>
        public LeaveRequestAlreadyProcessedException()
            : base("The leave request has already been approved or rejected.")
        {
        }
    }
}
