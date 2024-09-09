namespace ProductFeatureManagementWebApi
{
    using Microsoft.Extensions.Logging;
    using ProductFeatureManagementWebApi.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly ILogger<StatusService> _logger;

        public StatusService(IStatusRepository statusRepository, ILogger<StatusService> logger)
        {
            _statusRepository = statusRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Status>> GetAllStatusesAsync()
        {
            _logger.LogInformation("Attempting to retrieve all statuses.");
            try
            {
                var statuses = await _statusRepository.GetStatusAsync();
                _logger.LogInformation("Successfully retrieved all statuses.");
                return statuses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving statuses.");
                throw;
            }
        }

        public async Task<Status> GetStatusByIdAsync(int statusId)
        {
            try
            {
                var status = await _statusRepository.GetStatusByIdAsync(statusId);
                if (status == null)
                {
                    _logger.LogWarning("Status with ID {StatusId} not found.", statusId);
                }
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving status with ID {StatusId}", statusId);
                throw;  // Rethrow the exception to let higher layers handle it
            }
        }
    }

}
