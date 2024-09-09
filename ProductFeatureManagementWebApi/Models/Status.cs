using System;
using System.Collections.Generic;

namespace ProductFeatureManagementWebApi.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Feature> Features { get; set; } = new List<Feature>();
}
