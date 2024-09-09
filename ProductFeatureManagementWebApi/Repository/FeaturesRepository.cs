using Microsoft.EntityFrameworkCore;
using ProductFeatureManagementWebApi.Models;

namespace ProductFeatureManagementWebApi
{
    public class FeaturesRepository : IFeaturesRepository
    {
        private readonly ProductFeatureMgmtDbContext _featureMgmtDbContext;
        private readonly ILogger<FeaturesRepository> _logger;
        public FeaturesRepository(ProductFeatureMgmtDbContext featureMgmtDbContext, ILogger<FeaturesRepository> logger)
        {
            _featureMgmtDbContext = featureMgmtDbContext;
            _logger = logger;
        }

        // Get all features (could be useful for listing them)
        public async Task<IEnumerable<FeatureDto>> GetAllFeaturesAsync()
        {
            _logger.LogInformation("Attempting to get all features.");
            try
            {
                var features = await _featureMgmtDbContext.Features
                               .Include(f => f.Status)       // Join Status table
                               .Include(f => f.Complexity) // Join Complexity table
                               .Select(f => new FeatureDto
                               {
                                   FeaturesId = f.FeaturesId,
                                   Title = f.Title,
                                   Description = f.Description,
                                   ComplexityId = f.Complexity != null ? f.Complexity.ComplexityId : (int?)null,
                                   ComplexityName = f.Complexity != null ? f.Complexity.ComplexityName : null,
                                   StatusId = f.Status != null ? f.Status.StatusId : (int?)null,
                                   StatusName = f.Status != null ? f.Status.StatusName : null,
                                   TargetCompletionDate = f.TargetCompletionDate,
                                   ActualCompletionDate = f.ActualCompletionDate
                               })
                               .ToListAsync();

                return features;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all features.");
                throw;
            }
        }

        // Get a feature by ID
        public async Task<Feature> GetFeatureByIdAsync(long featureId)
        {
            return await _featureMgmtDbContext.Features.FindAsync(featureId); // Find feature by ID
        }

        // Add a new feature
        public async Task<Feature> AddFeatureAsync(Feature feature)
        {
            _logger.LogInformation("Attempting to add a new feature.");
            try
            {
                _featureMgmtDbContext.Features.Add(feature);
                await _featureMgmtDbContext.SaveChangesAsync(); // Save changes to the database
                _logger.LogInformation("Feature added successfully with ID: {FeaturesId}", feature.FeaturesId);

                return feature; // Return the added feature with its generated ID
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
                var existingFeature = await _featureMgmtDbContext.Features.FindAsync(feature.FeaturesId);
                if (existingFeature == null)
                {
                    return null; // Feature not found
                }

                // Update the fields
                existingFeature.Title = feature.Title;
                existingFeature.Description = feature.Description;
                existingFeature.ComplexityId = feature.ComplexityId;
                existingFeature.StatusId = feature.StatusId;
                existingFeature.TargetCompletionDate = feature.TargetCompletionDate;
                existingFeature.ActualCompletionDate = feature.ActualCompletionDate;

                await _featureMgmtDbContext.SaveChangesAsync(); // Save changes to the database
                _logger.LogInformation("Feature with ID: {FeatureId} updated successfully.", feature.FeaturesId);

                return existingFeature; // Return the updated feature
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating feature with ID: {FeaturesId}", feature.FeaturesId);
                throw;
            }
        }

        // Delete a feature by its ID
        public async Task<bool> DeleteFeatureAsync(long featureId)
        {
            _logger.LogInformation("Attempting to delete feature with ID: {FeatureId}", featureId);
            try
            {
                var feature = await _featureMgmtDbContext.Features.FindAsync(featureId);
                if (feature == null)
                {
                    _logger.LogWarning("Feature with ID: {FeatureId} not found.", featureId);

                    return false; // Feature not found
                }

                _featureMgmtDbContext.Features.Remove(feature); // Remove the feature
                await _featureMgmtDbContext.SaveChangesAsync(); // Save changes to the database
                _logger.LogInformation("Feature with ID: {FeatureId} deleted successfully.", featureId);

                return true; // Return success
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting feature with ID: {FeatureId}", featureId);
                throw;
            }
        }

    }
}
