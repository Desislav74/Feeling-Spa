using System.Collections.Generic;

namespace FeelingSpa.Services.Data.SalonServices
{
    using System.Threading.Tasks;

    public interface ISalonServicesService
    {
        Task<T> GetByIdAsync<T>(string salonId, int serviceId);

        Task CreateAsync(string salonId, IEnumerable<int> servicesIds);

        Task CreateAsync(IEnumerable<string> salonsIds, int serviceId);

        Task ChangeAvailableStatusAsync(string salonId, int serviceId);
    }
}
