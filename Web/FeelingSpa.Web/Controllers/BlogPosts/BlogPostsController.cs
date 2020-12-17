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

namespace FeelingSpa.Web.Controllers.BlogPosts
{
    public class BlogPostsController : Controller
    {
        private readonly IBlogPostsService blogPostsService;
        private readonly IWebHostEnvironment environment;

        public BlogPostsController(IBlogPostsService blogPostsService, IWebHostEnvironment environment)
        {
            this.blogPostsService = blogPostsService;
            this.environment = environment;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogPostInputModel input, string image)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.blogPostsService.CreateAsync(input, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(All));
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 6;
            var viewModel = new BlogPostsListViewModel()
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                BlogPosts = this.blogPostsService.GetAll<BlogPostViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlogPosts(int id)
        {
            await this.blogPostsService.DeleteAsync(id);

            return this.Redirect("/BlogPosts/All");
        }

        public async Task<IActionResult> Index(int? sortId) // blogPostId
        {
            if (sortId != null)
            {
                var blogPost = await this.blogPostsService
                    .GetByIdAsync<BlogPostViewModel>(sortId.Value);
                if (blogPost == null)
                {
                    return new StatusCodeResult(404);
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

        [HttpPost]
        public async Task<IActionResult> SingleBlog(int id)
        {
          var viewModel = await this.blogPostsService.GetByIdAsync<BlogPostViewModel>(id);
          return this.View(viewModel);
        }
    }
}
