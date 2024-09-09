using ProductFeatureManagementWebApi.CustomValidations;
using ProductFeatureManagementWebApi.CustomValidations.YourNamespace.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductFeatureManagementWebApi.Models;

public partial class Feature
{

    public long FeaturesId { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Title { get; set; } = null!;

    [MaxLength(5000)]
    public string? Description { get; set; }

    [Required]
    public int? ComplexityId { get; set; }

    [Required]
    public int? StatusId { get; set; }

    [RequiredIfActiveAttribute]
    public DateTime? TargetCompletionDate { get; set; }

    [RequiredIfClosedAttribute]
    public DateTime? ActualCompletionDate { get; set; }

    public virtual Complexity? Complexity { get; set; }

    public virtual Status? Status { get; set; }
}
