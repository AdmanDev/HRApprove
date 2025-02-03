namespace HRApprove.Tests.Domain.Entities
{
    using HRApprove.Domain.Entities;
    using HRApprove.Domain.Exceptions;
    using HRApprove.Domain.ValueObjects;

    public class LeaveRequestTests
    {
        private Employee GetEmployee()
        {
            return new Employee(1, "John", "Doe", "john.doe@tf1.com", DateTime.Now, []);
        }

        private LeaveType GetLeaveType()
        {
            return new LeaveType(1, "Vacation");
        }

        #region Constructor

        [Fact]
        public void LeaveRequest_ShouldBeInstantiated_WhenDataIsValid()
        {
            // Arrange
            Employee employee = this.GetEmployee();
            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = startDate.AddDays(10);
            LeaveType leaveType = this.GetLeaveType();
            string comment = "Needs a vacation";

            // Act
            LeaveRequest leaveRequest = new LeaveRequest(employee, leaveType, startDate, endDate, comment);

            // Assert
            Assert.Equal(employee, leaveRequest.Employee);
            Assert.Equal(leaveType, leaveRequest.LeaveType);
            Assert.Equal(startDate, leaveRequest.StartDate);
            Assert.Equal(endDate, leaveRequest.EndDate);
            Assert.Equal(comment, leaveRequest.Comment);

        }

        [Fact]
        public void LeaveRequest_ShouldThrowDateRangeException_WhenDateRangeIsInvalid()
        {
            // Arrange
            Employee employee = this.GetEmployee();
            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = startDate.AddDays(-1);
            LeaveType leaveType = this.GetLeaveType();
            string comment = "Needs a vacation";

            // Act & Assert
            Assert.Throws<DateRangeException>(
                () => new LeaveRequest(employee, leaveType, startDate, endDate, comment));
        }

        #endregion

        #region ProcessApproval Tests

        [Fact]
        public void ProcessApproval_ShouldSetStatusToApproved_WhenGivingTrueParam()
        {
            // Arrange
            Employee employee = this.GetEmployee();
            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = startDate.AddDays(10);
            LeaveType leaveType = this.GetLeaveType();
            string comment = "Needs a vacation";
            string hrComment = "Vacation approved";

            LeaveRequest leaveRequest = new LeaveRequest(employee, leaveType, startDate, endDate, comment);

            // Act
            leaveRequest.ProcessApproval(true, hrComment);

            // Assert
            Assert.Equal(LeaveStatus.Approved, leaveRequest.Status);
            Assert.Equal(hrComment, leaveRequest.HRComment);
        }

        [Fact]
        public void ProcessApproval_ShouldSetStatusToRejected_WhenGivingFalseParam()
        {
            // Arrange
            Employee employee = this.GetEmployee();
            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = startDate.AddDays(10);
            LeaveType leaveType = this.GetLeaveType();
            string comment = "Needs a vacation";
            string hrComment = "Vacation approved";

            LeaveRequest leaveRequest = new LeaveRequest(employee, leaveType, startDate, endDate, comment);

            // Act
            leaveRequest.ProcessApproval(false, hrComment);

            // Assert
            Assert.Equal(LeaveStatus.Rejected, leaveRequest.Status);
            Assert.Equal(hrComment, leaveRequest.HRComment);
        }

        [Fact]
        public void ProcessApproval_ShouldThrowException_WhenApprovalIsAlreadyProcessed()
        {
            // Arrange
            Employee employee = this.GetEmployee();
            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = startDate.AddDays(10);
            LeaveType leaveType = this.GetLeaveType();
            string comment = "Needs a vacation";
            string hrComment = "Vacation approved";

            LeaveRequest leaveRequest = new LeaveRequest(1, employee, leaveType, startDate, endDate, LeaveStatus.Approved, comment);

            // Act & Assert
            Assert.Throws<LeaveRequestAlreadyProcessedException>(
                () => leaveRequest.ProcessApproval(false, hrComment));
        }

        #endregion

    }
}
