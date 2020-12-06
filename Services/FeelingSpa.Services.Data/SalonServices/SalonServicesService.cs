using System.Collections.Generic;
using FeelingSpa.Web.ViewModels.SalonServices;

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

        public async Task CreateAsync(IEnumerable<string> salonsIds, int serviceId)
        {
            foreach (var salonId in salonsIds)
            {
                var salonService = new SalonService
                {
                    SalonId = salonId,
                    ServiceId = serviceId,
                    Available = true,
                };
                await this.salonServicesRepository.AddAsync(salonService);
                await this.salonServicesRepository.SaveChangesAsync();
            }
        }

        public async Task CreateAsync(string salonId, IEnumerable<int> servicesIds)
        {
            foreach (var serviceId in servicesIds)
            {
                var salonService = new SalonService
                {
                    SalonId = salonId,
                    ServiceId = serviceId,
                    Available = true,
                };
                await this.salonServicesRepository.AddAsync(salonService);
                await this.salonServicesRepository.SaveChangesAsync();
            }
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

        public async Task ChangeAvailableStatusAsync(string salonId, int serviceId)
        {
            var salonService =
                await this.salonServicesRepository
                    .All()
                    .Where(x => x.SalonId == salonId
                                && x.ServiceId == serviceId)
                    .FirstOrDefaultAsync();

            salonService.Available = !salonService.Available;

            await this.salonServicesRepository.SaveChangesAsync();
        }

    }
}
