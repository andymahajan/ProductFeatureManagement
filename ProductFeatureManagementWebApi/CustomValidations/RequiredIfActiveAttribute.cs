namespace ProductFeatureManagementWebApi.CustomValidations
{
    using ProductFeatureManagementWebApi.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RequiredIfActiveAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var feature = (Feature)validationContext.ObjectInstance;
            var statusService = (IStatusService)validationContext.GetService(typeof(IStatusService));

            if (statusService == null)
            {
                return new ValidationResult("Status service is not available.");
            }

            var status = statusService.GetStatusByIdAsync((int)feature.StatusId).Result;

            if (status != null && status.StatusName == "Active" && value == null)
            {
                return new ValidationResult("Target Completion Date is required when Status is Active.");
            }

            if (value != null && (DateTime)value <= DateTime.Now)
            {
                return new ValidationResult("Target Completion Date must be a future date.");
            }

            return ValidationResult.Success;
        }
    }

}
