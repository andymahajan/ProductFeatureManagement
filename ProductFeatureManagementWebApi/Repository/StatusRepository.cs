namespace ProductFeatureManagementWebApi
{
    using Microsoft.Extensions.Logging;
    using ProductFeatureManagementWebApi.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class StatusRepository : IStatusRepository
    {
        private readonly ProductFeatureMgmtDbContext _statusMgmtDbContext;
        private readonly ILogger<StatusRepository> _logger;

        public StatusRepository(ProductFeatureMgmtDbContext statusMgmtDbContext, ILogger<StatusRepository> logger)
        {
            _statusMgmtDbContext = statusMgmtDbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Status>> GetStatusAsync()
        {
            _logger.LogInformation("Attempting to retrieve all status records.");
            try
            {
                var statuses = await _statusMgmtDbContext.Statuses.ToListAsync();
                _logger.LogInformation("Successfully retrieved all status records.");
                return statuses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving status records.");
                throw;
            }
        }

        public async Task<Status> GetStatusByIdAsync(int statusId)
        {
            _logger.LogWarning("Get Status with ID {StatusId}.", statusId);
            try
            {
                return await _statusMgmtDbContext.Statuses
                    .AsNoTracking()  // Use AsNoTracking for read-only operations
                    .FirstOrDefaultAsync(s => s.StatusId == statusId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving status with ID {StatusId}", statusId);
                throw;
            }
        }
    }

}
