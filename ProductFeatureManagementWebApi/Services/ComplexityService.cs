namespace ProductFeatureManagementWebApi
{
    using Microsoft.Extensions.Logging;
    using ProductFeatureManagementWebApi.Models;
    using ProductFeatureManagementWebApi.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ComplexityService : IComplexityService
    {
        private readonly IComplexityRepository _complexityRepository;
        private readonly ILogger<ComplexityService> _logger;

        public ComplexityService(IComplexityRepository complexityRepository, ILogger<ComplexityService> logger)
        {
            _complexityRepository = complexityRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Complexity>> GetAllComplexitiesAsync()
        {
            _logger.LogInformation("Attempting to retrieve all complexities.");
            try
            {
                var complexities = await _complexityRepository.GetAllComplexityAsync();
                _logger.LogInformation("Successfully retrieved all complexities.");
                return complexities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving complexities.");
                throw;
            }
        }
    }
}
