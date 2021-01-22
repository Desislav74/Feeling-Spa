namespace FeelingSpa.Web.Controllers.Categories
{
    using System;
    using System.Threading.Tasks;

    using FeelingSpa.Services.Data.Categories;
    using FeelingSpa.Web.ViewModels.Categories;
    using FeelingSpa.Web.ViewModels.Category;
    using FeelingSpa.Web.ViewModels.Salons;
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

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 4;
            var viewModel = new CategoriesListViewModel()
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                SalonsCount = this.categoriesService.GetCount(),
                Categories = this.categoriesService.GetAll<CategoryInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult SingleCategory(int id)
        {
            var viewModel = this.categoriesService.GetById<CategoryInListViewModel>(id);
            if (viewModel == null)
            {
                return this.RedirectToAction("Error404", "Home");
            }

            return this.View(viewModel);
        }
    }
}
