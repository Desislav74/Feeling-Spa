using FeelingSpa.Web.ViewModels.Salons;

namespace FeelingSpa.Web.Controllers.Categories
{
    using System;
    using System.Threading.Tasks;

    using FeelingSpa.Services.Data.Categories;
    using FeelingSpa.Web.ViewModels.Category;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IWebHostEnvironment environment;

        public CategoriesController(ICategoriesService categoriesService, IWebHostEnvironment environment)
        {
            this.categoriesService = categoriesService;
            this.environment = environment;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryInputModel input, string image)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
              await this.categoriesService.CreateAsync(input, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
               this.ModelState.AddModelError(string.Empty, ex.Message);
               return this.View(input);
            }

            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 6;
            var viewModel = new CategoriesListViewModel()
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                SalonsCount = this.categoriesService.GetCount(),
                Categories = this.categoriesService.GetAll<CategoryInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult CategoryById(int id)
        {
            var viewModel = this.categoriesService.GetById<CategoryInListViewModel>(id);
            return this.Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await this.categoriesService.DeleteAsync(id);

            return this.RedirectToAction("/");
        }
    }
}
