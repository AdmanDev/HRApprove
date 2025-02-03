namespace HRApprove.Application.Commands.SubmitLeaveRequest
{
    using System.ComponentModel.DataAnnotations;
    using HRApprove.Application.DTOs;
    using MediatR;

    /// <summary>
    /// Represents the submit leave request command.
    /// </summary>
    public class SubmitLeaveRequestCommand : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitLeaveRequestCommand"/> class.
        /// </summary>
        /// <param name="leaveRequest">The leave request details.</param>
        public SubmitLeaveRequestCommand(CreateLeaveRequestDto leaveRequest)
        {
            this.LeaveRequest = leaveRequest;
        }

        /// <summary>
        /// Gets or sets the leave request.
        /// </summary>
        [Required(ErrorMessage = "The leave request details field is required")]
        public CreateLeaveRequestDto LeaveRequest { get; set; }
    }
}
