namespace HRApprove.Application.Validations
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a validation attribute for required date time value.
    /// </summary>
    public class FuturDateTimeAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is DateTime dateTime)
            {
                return dateTime >= DateTime.Now;
            }

            return false;
        }
    }
}
