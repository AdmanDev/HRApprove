namespace HRApprove.API.Controllers
{
    using HRApprove.Application.Commands.ReviewLeaveRequest;
    using HRApprove.Application.Commands.SubmitLeaveRequest;
    using HRApprove.Application.DTOs;
    using HRApprove.Application.Queries.LeaveRequest.GetAll;
    using HRApprove.Application.Queries.LeaveRequest.GetEmployeeLeaveRequests;
    using HRApprove.Application.Responses;
    using HRApprove.Application.Responses.Bases;
    using HRApprove.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Represents a controller for handling leave requests.
    /// </summary>
    public class LeaveRequestController : BaseApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequestController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator service.</param>
        public LeaveRequestController(IMediator mediator)
            : base(mediator)
        {
        }

        /// <summary>
        /// Gets all leave requests.
        /// </summary>
        /// <returns>A list of leave requests.</returns>
        [HttpGet]
        [ProducesResponseType<LeaveRequestListResponse>(200)]
        public async Task<IActionResult> GetAllLeaveRequests()
        {
            IEnumerable<LeaveRequestDto> leaveRequests = await this.Mediator.Send(new GetAllLeaveRequestsQuery());
            return this.Ok(new LeaveRequestListResponse(leaveRequests));
        }

        /// <summary>
        /// Gets leave requests of a specific employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>A list of leave requests for the employee.</returns>
        [HttpGet("employee/{employeeId}")]
        [ProducesResponseType<LeaveRequestListResponse>(200)]
        public async Task<IActionResult> GetEmployeeLeaveRequests(int employeeId)
        {
            GetEmployeeLeaveRequestsQuery query = new GetEmployeeLeaveRequestsQuery() { EmployeeId = employeeId };
            IEnumerable<LeaveRequestDto> leaveRequests = await this.Mediator.Send(query);

            return this.Ok(new LeaveRequestListResponse(leaveRequests));
        }

        /// <summary>
        /// Submits a leave request.
        /// </summary>
        /// <param name="request">The leave request details.</param>
        /// <returns>A default API response.</returns>
        [HttpPost]
        [ProducesResponseType<DefaultApiResponse>(201)]
        [ProducesResponseType<ErrorApiResponse>(400)]
        public async Task<IActionResult> SubmitLeaveRequest([FromBody] SubmitLeaveRequestCommand request)
        {
            await this.Mediator.Send(request);
            return this.StatusCode(201, new DefaultApiResponse());
        }

        /// <summary>
        /// Approves or rejects a leave request.
        /// </summary>
        /// <param name="request">The leave request review details.</param>
        /// <returns>A default API response.</returns>
        [HttpPut("review")]
        [ProducesResponseType<DefaultApiResponse>(200)]
        [ProducesResponseType<ErrorApiResponse>(400)]
        public async Task<IActionResult> ReviewLeaveRequest([FromBody] ReviewLeaveRequestCommand request)
        {
            await this.Mediator.Send(request);
            return this.StatusCode(200, new DefaultApiResponse());
        }
    }
}
