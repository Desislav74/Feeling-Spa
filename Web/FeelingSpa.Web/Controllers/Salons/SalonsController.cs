using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace FeelingSpa.Web.Controllers.Salons
{
    using FeelingSpa.Services.Data.Categories;
    using FeelingSpa.Services.Data.Salons;
    using FeelingSpa.Web.ViewModels.Salons;
    using Microsoft.AspNetCore.Mvc;

    public class SalonsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly ISalonsService salonsService;
        private readonly IWebHostEnvironment environment;

        public SalonsController(ICategoriesService categoriesService, ISalonsService salonsService, IWebHostEnvironment environment)
        {
            this.categoriesService = categoriesService;
            this.salonsService = salonsService;
            this.environment = environment;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateSalonInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSalonInputModel input, string image)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            try
            {
                await this.salonsService.CreateAsync(input, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            return this.Redirect("/");
        }
    }
}
