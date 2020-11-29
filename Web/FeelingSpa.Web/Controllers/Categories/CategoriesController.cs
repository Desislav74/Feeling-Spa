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
    }
}
