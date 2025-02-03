namespace HRApprove.Tests.Application.Queries.GetEmployeeLeaveRequest
{
    using HRApprove.Application.DTOs;
    using HRApprove.Application.Extensions;
    using HRApprove.Application.Queries.LeaveRequest.GetEmployeeLeaveRequests;
    using HRApprove.Domain.Entities;
    using HRApprove.Domain.Interfaces.Repositories;
    using Moq;

    public class GetAllLeaveRequestsQueryHandlerTests
    {
        private readonly Mock<ILeaveRequestRepository> leaveRequestRepositoryMock;

        private readonly GetEmployeeLeaveRequestsQueryHandler handler;

        public GetAllLeaveRequestsQueryHandlerTests()
        {
            leaveRequestRepositoryMock = new Mock<ILeaveRequestRepository>();

            handler = new GetEmployeeLeaveRequestsQueryHandler(leaveRequestRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnLeaveRequests()
        {
            // Arrange
            GetEmployeeLeaveRequestsQuery query = new GetEmployeeLeaveRequestsQuery() { EmployeeId = 1 };

            Employee employee = new Employee(query.EmployeeId, "John", "Doe", "john.doe@tf1.com", DateTime.Now, []);
            LeaveType leaveType = new LeaveType(0, "Vacation");
            LeaveRequest leaveRequest = new LeaveRequest(employee, leaveType, DateTime.Now, DateTime.Now, null);

            List<LeaveRequest> leaveRequestList = new List<LeaveRequest>()
            {
                leaveRequest
            };

            IEnumerable<LeaveRequestDto> expectedResult = leaveRequestList.Select(lr => lr.ToDto());

            leaveRequestRepositoryMock
                .Setup(x => x.GetByEmployeeIdAsync(query.EmployeeId))
                .ReturnsAsync(leaveRequestList);

            // Act
            IEnumerable<LeaveRequestDto> actualResult = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equivalent(expectedResult, actualResult);
        }
    }
}
