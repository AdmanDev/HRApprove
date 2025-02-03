namespace HRApprove.API.Filters
{
    using HRApprove.Application.Exceptions;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Represents a filter for validating model attributes.
    /// </summary>
    public class ValidateModelAttributeFilter : ActionFilterAttribute
    {
        /// <inheritdoc/>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                Dictionary<string, string[]> errors = context.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

                throw new DataValidationException(errors);
            }
        }
    }
}
