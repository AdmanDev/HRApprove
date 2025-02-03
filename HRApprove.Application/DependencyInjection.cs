namespace HRApprove.Application
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Represents the dependency injection for application layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the application layer services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection with the application layer.</returns>
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            return services;
        }
    }
}
