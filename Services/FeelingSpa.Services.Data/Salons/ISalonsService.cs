namespace FeelingSpa.Services.Data.Salons
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FeelingSpa.Web.ViewModels.Salons;

    public interface ISalonsService
    {
        Task CreateAsync(CreateSalonInputModel input, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();

        T GetById<T>(string id);
    }
}
