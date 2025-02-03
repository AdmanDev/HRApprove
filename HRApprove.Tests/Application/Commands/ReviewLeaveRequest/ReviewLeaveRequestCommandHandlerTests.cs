namespace HRApprove.Tests.Application.Commands.ReviewLeaveRequest
{
    using HRApprove.Application.Commands.ReviewLeaveRequest;
    using HRApprove.Domain.Entities;
    using HRApprove.Domain.Exceptions.Bases;
    using HRApprove.Domain.Interfaces.Repositories;
    using HRApprove.Domain.Interfaces.Services;
    using Moq;

    public class ReviewLeaveRequestCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> employeeRepositoryMock;
        private readonly Mock<ILeaveRequestRepository> leaveRequestRepositoryMock;
        private readonly Mock<ILeaveApprovalService> leaveApprovalServiceMock;

        private readonly ReviewLeaveRequestCommandHandler handler;

        public ReviewLeaveRequestCommandHandlerTests()
        {
            this.employeeRepositoryMock = new Mock<IEmployeeRepository>();
            this.leaveRequestRepositoryMock = new Mock<ILeaveRequestRepository>();
            this.leaveApprovalServiceMock = new Mock<ILeaveApprovalService>();

            this.handler = new ReviewLeaveRequestCommandHandler(
                this.employeeRepositoryMock.Object,
                this.leaveRequestRepositoryMock.Object,
                this.leaveApprovalServiceMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidData_ShouldProcessApproval()
        {
            // Arrange
            Employee employee = new Employee(1, "John", "Doe", "john.doe@tf1.com", DateTime.Now, []);
            DateTime startDate = DateTime.UtcNow.AddDays(1);
            DateTime endDate = startDate.AddDays(5);
            LeaveType leaveType = new LeaveType(1, "Vacation");

            LeaveRequest leaveRequest = new LeaveRequest(employee, leaveType, startDate, endDate, "Needs a vacation");

            Role hrRole = new Role(1, "HR");
            Employee hrManager = new Employee(1, "Mary", "Smith", "mary.smith@tf1.com", DateTime.Now, [hrRole]);

            ReviewLeaveRequestCommand command = new ReviewLeaveRequestCommand()
            {
                ApproverId = hrManager.EmployeeId,
                LeaveRequestId = leaveRequest.LeaveRequestId,
                IsApproved = true,
                Reason = "Vacation approved"
            };

            this.employeeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(hrManager.EmployeeId))
                .ReturnsAsync(hrManager);

            this.leaveApprovalServiceMock
                .Setup(service => service.ReviewLeaveRequest(hrManager, leaveRequest, command.IsApproved, command.Reason));
            
            this.leaveRequestRepositoryMock
                .Setup(repo => repo.GetByIdAsync(leaveRequest.LeaveRequestId))
                .ReturnsAsync(leaveRequest);

            this.leaveRequestRepositoryMock
                .Setup(repo => repo.UpdateAsync(leaveRequest))
                .Returns(Task.CompletedTask);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.leaveApprovalServiceMock.Verify(service => service.ReviewLeaveRequest(hrManager, leaveRequest, command.IsApproved, command.Reason), Times.Once);
            this.leaveRequestRepositoryMock.Verify(repo => repo.UpdateAsync(leaveRequest), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenApproverNotFound()
        {
            // Arrange
            ReviewLeaveRequestCommand command = new ReviewLeaveRequestCommand()
            {
                ApproverId = 1,
                LeaveRequestId = 1,
                IsApproved = true,
                Reason = "Vacation approved"
            };

            this.employeeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Employee?)null);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedException>(
                () => this.handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenLeaveRequestNotFound()
        {
            // Arrange
            Role hrRole = new Role(1, "HR");
            Employee hrManager = new Employee(1, "Mary", "Smith", "mary.smith@tf1.com", DateTime.Now, [hrRole]);

            ReviewLeaveRequestCommand command = new ReviewLeaveRequestCommand()
            {
                ApproverId = hrManager.EmployeeId,
                LeaveRequestId = 1,
                IsApproved = true,
                Reason = "Vacation approved"
            };

            this.employeeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(hrManager.EmployeeId))
                .ReturnsAsync(hrManager);

            this.leaveRequestRepositoryMock
                .Setup(repo => repo.GetByIdAsync(command.LeaveRequestId))
                .ReturnsAsync((LeaveRequest?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(
                () => this.handler.Handle(command, CancellationToken.None));
        }

    }
}
