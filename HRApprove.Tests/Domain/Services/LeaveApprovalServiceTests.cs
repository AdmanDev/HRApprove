namespace HRApprove.Tests.Domain.Services
{
    using HRApprove.Domain.Entities;
    using HRApprove.Domain.Exceptions.Bases;
    using HRApprove.Domain.Services;
    using HRApprove.Domain.ValueObjects;
    using Moq;

    public class LeaveApprovalServiceTests
    {
        private readonly LeaveApprovalService leaveApprovalService;

        public LeaveApprovalServiceTests()
        {
            leaveApprovalService = new LeaveApprovalService();
        }

        [Fact]
        public void ReviewLeaveRequest_ShouldProcessApproval_WhenAllConditionsAreMet()
        {
            // Arrange
            Role role = new Role(1, "HR");
            Employee approver = new Employee(1, "John", "Doe", "john.doe@tf1.com", DateTime.Now, [role]);
            Employee employee = new Employee(2, "Jane", "Doe", "jane.doe@tf1.com", DateTime.Now, []);

            LeaveType leaveType = new LeaveType(1, "Sick");
            LeaveRequest leaveRequest = new LeaveRequest(employee, leaveType, DateTime.Now, DateTime.Now.AddDays(5));

            bool isApproved = true;
                    
            // Act
            leaveApprovalService.ReviewLeaveRequest(approver, leaveRequest, isApproved, null);

            // Assert
            Assert.Equal(LeaveStatus.Approved, leaveRequest.Status);
        }

        [Fact]
        public void ReviewLeaveRequest_ShouldThrowUnauthorizedException_WhenApproverIsNotHR()
        {
            // Arrange
            Employee approver = new Employee(1, "John", "Doe", "john.doe@tf1.com", DateTime.Now, []);
            Employee employee = new Employee(2, "Jane", "Doe", "jane.doe@tf1.com", DateTime.Now, []);

            LeaveType leaveType = new LeaveType(1, "Sick");
            LeaveRequest leaveRequest = new LeaveRequest(employee, leaveType, DateTime.Now, DateTime.Now.AddDays(5));

            bool isApproved = true;

            // Act & Assert
            Assert.Throws<UnauthorizedException>(
                () => leaveApprovalService.ReviewLeaveRequest(approver, leaveRequest, isApproved, null));
        }

        [Fact]
        public void ReviewLeaveRequest_ShouldThrowBadRequestException_WhenApproverIsTheRequester()
        {
            // Arrange
            Role role = new Role(1, "HR");
            Employee approver = new Employee(1, "John", "Doe", "john.doe@tf1.com", DateTime.Now, [role]);

            LeaveType leaveType = new LeaveType(1, "Sick");
            LeaveRequest leaveRequest = new LeaveRequest(approver, leaveType, DateTime.Now, DateTime.Now.AddDays(5));

            bool isApproved = true;

            // Act & Assert
            Assert.Throws<BadRequestException>(
                () => leaveApprovalService.ReviewLeaveRequest(approver, leaveRequest, isApproved, null));
        }
    }
}
