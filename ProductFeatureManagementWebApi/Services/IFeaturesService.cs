using ProductFeatureManagementWebApi.Models;

namespace ProductFeatureManagementWebApi
{
    public interface IFeaturesService
    {
        public Task<IEnumerable<FeatureDto>> GetAllFeaturesAsync();
        public Task<Feature> GetFeatureByIdAsync(long featureId);
        public Task<Feature> AddFeatureAsync(Feature feature);
        public Task<Feature> UpdateFeatureAsync(Feature feature);
        public Task<bool> DeleteFeatureAsync(long featureId);
    }
}
