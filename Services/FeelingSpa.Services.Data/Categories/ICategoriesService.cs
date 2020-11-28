namespace FeelingSpa.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FeelingSpa.Web.ViewModels.Category;

    public interface ICategoriesService
    {
        Task CreateAsync(CreateCategoryInputModel input);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
