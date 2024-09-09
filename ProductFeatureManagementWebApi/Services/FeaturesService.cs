namespace ProductFeatureManagementWebApi
{
    using Microsoft.Extensions.Logging;
    using ProductFeatureManagementWebApi.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FeaturesService : IFeaturesService
    {
        private readonly IFeaturesRepository _repository;
        private readonly ILogger<FeaturesService> _logger;

        public FeaturesService(IFeaturesRepository repository, ILogger<FeaturesService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // Get all features
        public async Task<IEnumerable<FeatureDto>> GetAllFeaturesAsync()
        {
            _logger.LogInformation("Attempting to retrieve all features.");
            try
            {
                var features = await _repository.GetAllFeaturesAsync();
                _logger.LogInformation("Successfully retrieved all features.");
                return features;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all features.");
                throw;
            }
        }

        // Get a feature by ID
        public async Task<Feature> GetFeatureByIdAsync(long featureId)
        {
            _logger.LogInformation("Attempting to retrieve feature with ID: {FeatureId}", featureId);
            try
            {
                var feature = await _repository.GetFeatureByIdAsync(featureId);
                if (feature == null)
                {
                    _logger.LogWarning("Feature with ID: {FeatureId} not found.", featureId);
                }
                else
                {
                    _logger.LogInformation("Successfully retrieved feature with ID: {FeatureId}.", featureId);
                }
                return feature;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving feature with ID: {FeatureId}", featureId);
                throw;
            }
        }

        // Add a new feature
        public async Task<Feature> AddFeatureAsync(Feature feature)
        {
            _logger.LogInformation("Attempting to add a new feature.");
            try
            {
                var addedFeature = await _repository.AddFeatureAsync(feature);
                _logger.LogInformation("Successfully added feature with ID: {FeaturesId}", addedFeature.FeaturesId);
                return addedFeature;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new feature.");
                throw;
            }
        }

        // Update an existing feature
        public async Task<Feature> UpdateFeatureAsync(Feature feature)
        {
            _logger.LogInformation("Attempting to update feature with ID: {FeaturesId}", feature.FeaturesId);
            try
            {
                var updatedFeature = await _repository.UpdateFeatureAsync(feature);
                if (updatedFeature == null)
                {
                    _logger.LogWarning("Feature with ID: {FeaturesId} not found for update.", feature.FeaturesId);
                }
                else
                {
                    _logger.LogInformation("Successfully updated feature with ID: {FeaturesId}.", feature.FeaturesId);
                }
                return updatedFeature;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating feature with ID: {FeaturesId}", feature.FeaturesId);
                throw;
            }
        }

        // Delete a feature by ID
        public async Task<bool> DeleteFeatureAsync(long featureId)
        {
            _logger.LogInformation("Attempting to delete feature with ID: {FeatureId}", featureId);
            try
            {
                var result = await _repository.DeleteFeatureAsync(featureId);
                if (result)
                {
                    _logger.LogInformation("Successfully deleted feature with ID: {FeatureId}.", featureId);
                }
                else
                {
                    _logger.LogWarning("Feature with ID: {FeatureId} not found for deletion.", featureId);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting feature with ID: {FeatureId}", featureId);
                throw;
            }
        }
    }

}
