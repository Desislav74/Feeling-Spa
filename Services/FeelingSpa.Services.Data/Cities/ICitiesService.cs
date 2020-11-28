namespace FeelingSpa.Services.Data.Cities
{
    using System.Threading.Tasks;

    using FeelingSpa.Web.ViewModels.City;

    public interface ICitiesService
    {
        Task CreateAsync(CreateCityInputModel input);
    }
}
