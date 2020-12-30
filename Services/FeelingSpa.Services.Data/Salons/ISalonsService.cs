namespace FeelingSpa.Services.Data.Salons
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FeelingSpa.Web.ViewModels.Salons;

    public interface ISalonsService
    {
        Task<string> CreateAsync(CreateSalonInputModel input, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6);

        int GetCount();

        T GetById<T>(string id);

        Task<T> GetByIdAsync<T>(string id);

        Task DeleteAsync(string id);

        Task UpdateAsync(string id, CreateEditInputViewModel input);

        IEnumerable<T> GetByCategories<T>(IEnumerable<int> categoriesIds);

        Task RateSalonAsync(string id, int rateValue);

        Task<IEnumerable<T>> GetAllAsync<T>();

    }
}
