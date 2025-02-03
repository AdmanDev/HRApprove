namespace HRApprove.API.Middlewares
{
    using System;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;
    using HRApprove.Application.Exceptions;
    using HRApprove.Application.Responses.Bases;
    using HRApprove.Domain.Exceptions.Bases;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Middleware for handling and send errors response.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The request delegate.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invoke the middleware.
        /// </summary>
        /// <param name="context">The http context.</param>
        /// <returns>Nothing.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ConflictException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            string errorMessage = "An internal server error occurred.";

            if (exception is DataValidationException dataValidationException)
            {
                errorMessage = string.Join(" | ", dataValidationException.Errors
                    .SelectMany(e => e.Value));
            }
            else if (exception is HRException apiException)
            {
                errorMessage = apiException.Message;
            }

            ErrorApiResponse response = new ErrorApiResponse(errorMessage);

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
