using ProductFeatureManagementWebApi.Models;

namespace ProductFeatureManagementWebApi.Services
{
    public interface IComplexityService
    {
        Task<IEnumerable<Complexity>> GetAllComplexitiesAsync();
    }
}
