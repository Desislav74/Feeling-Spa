using System.Data;

namespace FeelingSpa.Services.Data.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using FeelingSpa.Web.ViewModels.Services;

    public interface IServicesService
    {
        Task CreateAsync(ServiceInputModel input);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task DeleteAsync(int id);
    }
}
