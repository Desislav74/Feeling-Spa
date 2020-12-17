using System.ComponentModel;
using System.Threading.Tasks;
using FeelingSpa.Services.Data.Blogposts;
using FeelingSpa.Web.ViewModels.BlogPost;
using Microsoft.AspNetCore.Mvc;

namespace FeelingSpa.Web.Infrastructure.ViewComponents
{
    public class LatestBlogPostsViewComponent : ViewComponent
    {
        private readonly IBlogPostsService blogPostsService;

        public LatestBlogPostsViewComponent(IBlogPostsService blogPostsService)
        {
            this.blogPostsService = blogPostsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? count)
        {
            var viewModel = new BlogPostsListViewModel
            {
                BlogPosts = await this.blogPostsService.GetAllLatestAsync<BlogPostViewModel>(count),
            };

            return this.View(viewModel);
        }
    }
}
