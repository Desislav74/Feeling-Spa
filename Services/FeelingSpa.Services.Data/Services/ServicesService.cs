namespace FeelingSpa.Services.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FeelingSpa.Data.Common.Repositories;
    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Mapping;
    using FeelingSpa.Web.ViewModels.Services;
    using Microsoft.EntityFrameworkCore;

    public class ServicesService : IServicesService
    {
        private readonly IDeletableEntityRepository<Service> servicesRepository;

        public ServicesService(IDeletableEntityRepository<Service>servicesRepository)
        {
            this.servicesRepository = servicesRepository;
        }

        public async Task CreateAsync(ServiceInputModel input)
        {
            var service = new Service
            {
                Name = input.Name,
                Description = input.Description,
                CategoryId = input.CategoryId,
            };
            await this.servicesRepository.AddAsync(service);
            await this.servicesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var services =
                await this.servicesRepository
                    .All()
                    .OrderBy(x => x.CategoryId)
                    .ThenBy(x => x.Id)
                    .To<T>().ToListAsync();
            return services;
        }

        public async Task<IEnumerable<int>> GetAllIdsByCategoryAsync(int categoryId)
        {
            ICollection<int> servicesIds =
                await this.servicesRepository
                    .All()
                    .Where(x => x.CategoryId == categoryId)
                    .OrderBy(x => x.Id)
                    .Select(x => x.Id)
                    .ToListAsync();
            return servicesIds;
        }

        public async Task DeleteAsync(int id)
        {
            var service =
                await this.servicesRepository
                    .AllAsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            this.servicesRepository.Delete(service);
            await this.servicesRepository.SaveChangesAsync();
        }
    }
}
