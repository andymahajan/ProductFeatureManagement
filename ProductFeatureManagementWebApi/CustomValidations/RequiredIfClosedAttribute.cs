namespace ProductFeatureManagementWebApi.CustomValidations
{
    using ProductFeatureManagementWebApi.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    namespace YourNamespace.Attributes
    {
        public class RequiredIfClosedAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var feature = (Feature)validationContext.ObjectInstance;
                var statusService = (IStatusService)validationContext.GetService(typeof(IStatusService));

                if (statusService == null)
                {
                    return new ValidationResult("Status service is not available.");
                }

                if (feature.StatusId == null)
                {
                    return ValidationResult.Success;
                }

                var status = statusService.GetStatusByIdAsync((int)feature.StatusId).Result;

                if (status != null && status.StatusName == "Closed" && value == null)
                {
                    return new ValidationResult("Actual Completion Date is required when Status is Closed.");
                }

                if (value != null && (DateTime)value <= DateTime.Now)
                {
                    return new ValidationResult("Actual Completion Date must be a future date.");
                }

                return ValidationResult.Success;
            }
        }
    }

}
