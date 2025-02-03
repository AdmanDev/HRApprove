namespace HRApprove.Tests.Application.Commands.SubmitLeaveRequest
{
    using HRApprove.Application.Commands.SubmitLeaveRequest;
    using HRApprove.Application.DTOs;
    using HRApprove.Domain.Entities;
    using HRApprove.Domain.Exceptions.Bases;
    using HRApprove.Domain.Interfaces.Repositories;
    using Moq;

    public class SubmitLeaveRequestCommandHandlerTests
    {
        private readonly Mock<ILeaveRequestRepository> leaveRequestRepositoryMock;
        private readonly Mock<ILeaveTypeRepository> leaveTypeRepositoryMock;
        private readonly Mock<IEmployeeRepository> employeeRepositoryMock;

        private readonly SubmitLeaveRequestCommandHandler handler;

        public SubmitLeaveRequestCommandHandlerTests()
        {
            this.leaveRequestRepositoryMock = new Mock<ILeaveRequestRepository>();
            this.leaveTypeRepositoryMock = new Mock<ILeaveTypeRepository>();
            this.employeeRepositoryMock = new Mock<IEmployeeRepository>();

            this.handler = new SubmitLeaveRequestCommandHandler(
                leaveRequestRepositoryMock.Object,
                leaveTypeRepositoryMock.Object,
                employeeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_SubmitLeaveRequestAsync_WithValidData_ShouldAddLeaveRequestToRepository()
        {
            // Arrange
            Employee employee = new Employee(1, "John", "Doe", "john.doe@tf1.com", DateTime.Now, []);
            DateTime startDate = DateTime.UtcNow.AddDays(1);
            DateTime endDate = startDate.AddDays(5);
            LeaveType leaveType = new LeaveType(1, "Vacation");
            string comment = "Needs a vacation";

            CreateLeaveRequestDto leaveRequest = new CreateLeaveRequestDto()
            {
                EmployeeId = employee.EmployeeId,
                Type = leaveType.Label,
                StartDate = startDate,
                EndDate = endDate,
                Comment = comment
            };

            SubmitLeaveRequestCommand command = new SubmitLeaveRequestCommand(leaveRequest);

            this.employeeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(employee.EmployeeId))
                .ReturnsAsync(employee);

            this.leaveTypeRepositoryMock
                .Setup(repo => repo.GetByTypeLabel(leaveType.Label))
                .ReturnsAsync(leaveType);

            this.leaveRequestRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<LeaveRequest>()))
                .Returns(Task.CompletedTask);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.leaveRequestRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<LeaveRequest>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenEmployeeNotFound()
        {
            // Arrange
            Employee employee = new Employee(1, "John", "Doe", "john.doe@tf1.com", DateTime.Now, []);
            DateTime startDate = DateTime.UtcNow.AddDays(1);
            DateTime endDate = startDate.AddDays(5);
            LeaveType leaveType = new LeaveType(1, "Vacation");
            string comment = "Needs a vacation";

            CreateLeaveRequestDto leaveRequest = new CreateLeaveRequestDto()
            {
                EmployeeId = employee.EmployeeId,
                Type = leaveType.Label,
                StartDate = startDate,
                EndDate = endDate,
                Comment = comment
            };

            SubmitLeaveRequestCommand command = new SubmitLeaveRequestCommand(leaveRequest);

            this.employeeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(employee.EmployeeId))
                .ReturnsAsync((Employee?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(
                () => this.handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowBadRequestException_WhenLeaveTypeNotFound()
        {
            // Arrange
            Employee employee = new Employee(1, "John", "Doe", "john.doe@tf1.com", DateTime.Now, []);
            DateTime startDate = DateTime.UtcNow.AddDays(1);
            DateTime endDate = startDate.AddDays(5);
            LeaveType leaveType = new LeaveType(1, "Vacation");
            string comment = "Needs a vacation";

            CreateLeaveRequestDto leaveRequest = new CreateLeaveRequestDto()
            {
                EmployeeId = employee.EmployeeId,
                Type = leaveType.Label,
                StartDate = startDate,
                EndDate = endDate,
                Comment = comment
            };

            SubmitLeaveRequestCommand command = new SubmitLeaveRequestCommand(leaveRequest);

            this.employeeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(employee.EmployeeId))
                .ReturnsAsync(employee);

            this.leaveTypeRepositoryMock
                .Setup(repo => repo.GetByTypeLabel(leaveType.Label))
                .ReturnsAsync((LeaveType?)null);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(
                () => this.handler.Handle(command, CancellationToken.None));
        }
    }
}
