namespace HRApprove.Application.Commands.ReviewLeaveRequest
{
    using System.ComponentModel.DataAnnotations;
    using MediatR;

    /// <summary>
    /// Represents a command to review a leave request.
    /// </summary>
    public class ReviewLeaveRequestCommand : IRequest
    {
        /// <summary>
        /// Gets or sets the leave request ID to review.
        /// </summary>
        [Required(ErrorMessage = "The leave request id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The leave request id is invalid")]
        public int LeaveRequestId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the approver.
        /// </summary>
        [Required(ErrorMessage = "The approver id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The approver id is invalid")]
        public int ApproverId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the leave request is approved or rejected.
        /// </summary>
        [Required(ErrorMessage = "The is approved field is required")]
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets the reason for accepting or rejecting the leave request.
        /// </summary>
        public string? Reason { get; set; }
    }
}
