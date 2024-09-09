using ProductFeatureManagementWebApi.Models;

namespace ProductFeatureManagementWebApi
{
    public interface IFeaturesRepository
    {
        public Task<IEnumerable<Feature>> GetAllFeaturesAsync();
        public Task<Feature> GetFeatureByIdAsync(long featureId);
        public Task<Feature> AddFeatureAsync(Feature feature);
        public Task<Feature> UpdateFeatureAsync(Feature feature);
        public Task<bool> DeleteFeatureAsync(long featureId);
    }
}