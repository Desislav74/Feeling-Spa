namespace FeelingSpa.Services.Data.SalonServices
{
    using System.Linq;
    using System.Threading.Tasks;

    using FeelingSpa.Data.Common.Repositories;
    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class SalonServicesService : ISalonServicesService
    {
        private readonly IDeletableEntityRepository<SalonService> salonServicesRepository;

        public SalonServicesService(IDeletableEntityRepository<SalonService> salonServicesRepository)
        {
            this.salonServicesRepository = salonServicesRepository;
        }

        public async Task<T> GetByIdAsync<T>(string salonId, int serviceId)
        {
            var salonService =
                await this.salonServicesRepository
                    .All()
                    .Where(x => x.SalonId == salonId && x.ServiceId == serviceId)
                    .To<T>().FirstOrDefaultAsync();
            return salonService;
        }

    }
}
