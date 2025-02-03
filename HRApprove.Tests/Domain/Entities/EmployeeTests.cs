namespace HRApprove.Tests.Domain.Entities
{
    using HRApprove.Domain.Entities;

    public class EmployeeTests
    {
        [Fact]
        public void HasRole_ShouldReturnTrue_WhenEmployeeHasGivenRole()
        {
            // Arrange
            Role role = new Role(1, "HR");
            Employee employee = new Employee(1, "John", "Doe", "john.doe@tf1.com", DateTime.Now, [role]);

            // Act
            bool hasRole = employee.HasRole(role.Label);

            // Assert
            Assert.True(hasRole);
        }

        [Fact]
        public void HasRole_ShouldReturnFalse_WhenEmployeeDoesNotHaveGivenRole()
        {
            // Arrange
            Employee employee = new Employee(1, "John", "Doe", "john.doe@tf1.com", DateTime.Now, []);

            // Act
            bool hasRole = employee.HasRole("HR");

            // Assert
            Assert.False(hasRole);
        }
    }
}
