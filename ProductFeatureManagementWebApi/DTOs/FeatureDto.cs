namespace ProductFeatureManagementWebApi
{
    public class FeatureDto
    {
        public long FeaturesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ComplexityId { get; set; }
        public string? ComplexityName { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public DateTime? TargetCompletionDate { get; set; }
        public DateTime? ActualCompletionDate { get; set; }
    }
}
