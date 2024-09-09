namespace ProductFeatureManagementWebApi
{
    using ProductFeatureManagementWebApi.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IComplexityRepository
    {
        Task<IEnumerable<Complexity>> GetAllComplexityAsync();
    }
}
