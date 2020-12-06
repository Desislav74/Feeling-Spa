using System.Collections.Generic;

namespace FeelingSpa.Services.Data.Cities
{
    using System.Threading.Tasks;

    using FeelingSpa.Web.ViewModels.City;

    public interface ICitiesService
    {
        Task CreateAsync(CreateCityInputModel input);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
