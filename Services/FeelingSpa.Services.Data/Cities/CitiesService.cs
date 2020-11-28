using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using FeelingSpa.Data.Common.Repositories;
namespace FeelingSpa.Services.Data.Cities
{
    using FeelingSpa.Data.Models;
    using FeelingSpa.Web.ViewModels.City;

    public class CitiesService : ICitiesService
    {
        private readonly IDeletableEntityRepository<City> citiesRepository;

        public CitiesService(IDeletableEntityRepository<City> citiesRepository)
        {
            this.citiesRepository = citiesRepository;
        }

        public async Task CreateAsync(CreateCityInputModel input)
        {
            var city = new City
            {
                Name = input.Name,
            };
            await this.citiesRepository.AddAsync(city);
            await this.citiesRepository.SaveChangesAsync();
        }
    }
}
