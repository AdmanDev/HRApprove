namespace HRApprove.Infrastructure
{
    using HRApprove.Domain.Interfaces.Repositories;
    using HRApprove.Infrastructure.Configurations;
    using HRApprove.Infrastructure.Interfaces;
    using HRApprove.Infrastructure.Persistences.Connections;
    using HRApprove.Infrastructure.Persistences.Repositories;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Represents the dependency injection for infrastructure.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the infrastructure layer services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection with the infrastructure layer.</returns>
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string not found");
            }

            // Add database configuration and connection factory
            services.AddSingleton(new DatabaseConfiguration(connectionString));
            services.AddSingleton<IMySqlConnectionFactory, MySqlConnectionFactory>();

            // Add repositories
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddSingleton<ILeaveTypeRepository, LeaveTypeRepository>();

            return services;
        }
    }
}
