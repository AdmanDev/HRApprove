namespace HRApprove.Tests.Application.Queries.GetAll
{
    using HRApprove.Application.DTOs;
    using HRApprove.Application.Extensions;
    using HRApprove.Application.Queries.LeaveRequest.GetAll;
    using HRApprove.Domain.Entities;
    using HRApprove.Domain.Interfaces.Repositories;
    using Moq;
    using System.Collections.Generic;

    public class GetAllLeaveRequestsQueryHandlerTests
    {
        private readonly Mock<ILeaveRequestRepository> leaveRequestRepositoryMock;

        private readonly GetAllLeaveRequestsQueryHandler handler;

        public GetAllLeaveRequestsQueryHandlerTests()
        {
            leaveRequestRepositoryMock = new Mock<ILeaveRequestRepository>();

            handler = new GetAllLeaveRequestsQueryHandler(leaveRequestRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnLeaveRequests()
        {
            // Arrange
            GetAllLeaveRequestsQuery query = new GetAllLeaveRequestsQuery();

            Employee employee = new Employee(1, "John", "Doe", "john.doe@tf1.com", DateTime.Now, []);
            LeaveType leaveType = new LeaveType(0, "Vacation");
            LeaveRequest leaveRequest = new LeaveRequest(employee, leaveType, DateTime.Now, DateTime.Now, null);

            List<LeaveRequest> leaveRequestList = new List<LeaveRequest>()
            {
                leaveRequest
            };

            IEnumerable<LeaveRequestDto> expectedResult = leaveRequestList.Select(lr => lr.ToDto());

            leaveRequestRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(leaveRequestList);

            // Act
            IEnumerable<LeaveRequestDto> actualResult = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equivalent(expectedResult, actualResult);
        }
    }
}
