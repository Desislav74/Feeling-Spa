using System.Collections.Generic;
using System.Threading.Tasks;
using FeelingSpa.Web.ViewModels.BlogPost;

namespace FeelingSpa.Services.Data.Blogposts
{
    public interface IBlogPostsService
    {
        Task CreateAsync(BlogPostInputModel input, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6);

        int GetCount();

        Task<T> GetByIdAsync<T>(int id);

        Task DeleteAsync(int id);

        Task UpdateAsync(int id, BlogPostInputModel input);

        Task<IEnumerable<T>> GetAllLatestAsync<T>(int? count = null);

        Task<IEnumerable<T>> GetAllWithSingleAsync<T>(int? sortId);
    }
}
