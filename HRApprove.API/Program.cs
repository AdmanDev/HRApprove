namespace HRApprove.API
{
    using System.Reflection;
    using HRApprove.API.Filters;
    using HRApprove.API.Middlewares;
    using HRApprove.Application;
    using HRApprove.Domain.Interfaces.Services;
    using HRApprove.Domain.Services;
    using HRApprove.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// The main program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            WebApplication builder = CreateHostBuilder(args).Build();

            ConfigureMiddleware(builder);

            builder.Run();
        }

        private static WebApplicationBuilder CreateHostBuilder(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            return builder;
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddInfrastructureLayer(configuration);
            services.AddApplicationLayer();

            // Add domain services
            services.AddSingleton<ILeaveApprovalService, LeaveApprovalService>();

            // Add controllers and behaviors config
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelAttributeFilter>();
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Swagger/OpenAPI
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HRApprove API",
                    Version = "v1",
                    Description = "An API to manage employees leave requests.",
                    Contact = new OpenApiContact
                    {
                        Name = "Atmaniou Adam",
                        Url = new Uri("https://admandev.fr"),
                    },
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}