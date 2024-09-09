using ProductFeatureManagementWebApi.Models;

namespace ProductFeatureManagementWebApi
{
    public interface IStatusRepository
    {
        public Task<IEnumerable<Status>> GetStatusAsync();
        public Task<Status> GetStatusByIdAsync(int statusId);
    }
}
