namespace FeelingSpa.Web.Controllers.BlogPosts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FeelingSpa.Services.Data.Blogposts;
    using FeelingSpa.Web.ViewModels.BlogPost;
    using FeelingSpa.Web.ViewModels.Category;
    using FeelingSpa.Web.ViewModels.Common;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class BlogPostsController : Controller
    {
        private readonly IBlogPostsService blogPostsService;
        private readonly IWebHostEnvironment environment;

        public BlogPostsController(IBlogPostsService blogPostsService, IWebHostEnvironment environment)
        {
            this.blogPostsService = blogPostsService;
            this.environment = environment;
        }

        public async Task<IActionResult> Index(int? sortId) // blogPostId
        {
            if (sortId != null)
            {
                var blogPost = await this.blogPostsService
                    .GetByIdAsync<BlogPostViewModel>(sortId.Value);
                if (blogPost == null)
                {
                    return this.RedirectToAction("Error404", "Home");
                }
            }

            this.ViewData["CurrentSort"] = sortId;

            var blogPosts = await this.blogPostsService
                .GetAllWithSingleAsync<BlogPostViewModel>(sortId);
            var blogPostsList = blogPosts.ToList();

            var count = this.blogPostsService.GetCount();

            var viewModel = new BlogPostsItemsListViewModel()
            {
                BlogPosts = new ItemsList<BlogPostViewModel>(blogPostsList, count),
            };

            return this.View(viewModel);
        }
    }
}
