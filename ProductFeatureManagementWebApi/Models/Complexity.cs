using System;
using System.Collections.Generic;

namespace ProductFeatureManagementWebApi.Models;

public partial class Complexity
{
    public int ComplexityId { get; set; }

    public string ComplexityName { get; set; } = null!;

    public virtual ICollection<Feature> Features { get; set; } = new List<Feature>();
}
