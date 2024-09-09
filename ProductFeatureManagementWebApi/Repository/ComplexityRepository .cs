namespace ProductFeatureManagementWebApi
{
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ProductFeatureManagementWebApi.Models;

    public class ComplexityRepository : IComplexityRepository
    {
        private readonly ProductFeatureMgmtDbContext _complexityMgmtDbContext;
        private readonly ILogger<ComplexityRepository> _logger;

        public ComplexityRepository(ProductFeatureMgmtDbContext complexityMgmtDbContext, ILogger<ComplexityRepository> logger)
        {
            _complexityMgmtDbContext = complexityMgmtDbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Complexity>> GetAllComplexityAsync()
        {
            _logger.LogInformation("Attempting to retrieve all complexity records.");
            try
            {
                var complexities = await _complexityMgmtDbContext.Complexities.ToListAsync();
                _logger.LogInformation("Successfully retrieved all complexity records.");
                return complexities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving complexity records.");
                throw;
            }
        }
    }

}
