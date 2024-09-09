namespace ProductFeatureManagementWebApi
{
    using ProductFeatureManagementWebApi.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStatusService
    {
        Task<IEnumerable<Status>> GetAllStatusesAsync();
        Task<Status> GetStatusByIdAsync(int statusId);
    }

}
