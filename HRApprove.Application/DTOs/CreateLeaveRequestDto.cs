namespace HRApprove.Application.DTOs
{
    using System.ComponentModel.DataAnnotations;
    using HRApprove.Application.Validations;

    /// <summary>
    /// Represents a data transfer object for submitting a leave request.
    /// </summary>
    public class CreateLeaveRequestDto
    {
        /// <summary>
        /// Gets or sets the ID of the employee submitting the leave request.
        /// </summary>
        [Required(ErrorMessage = "The employee id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The employee id is invalid")]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the type of leave requested.
        /// </summary>
        [Required(ErrorMessage = "The leave type is required")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start date of the leave request.
        /// </summary>
        [Required]
        [FuturDateTime(ErrorMessage = "The leave start date is required and must be in the future")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the leave request.
        /// </summary>
        [Required]
        [FuturDateTime(ErrorMessage = "The leave end date is required and must be in the future")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets a comment for the leave request.
        /// </summary>
        public string? Comment { get; set; }
    }
}
