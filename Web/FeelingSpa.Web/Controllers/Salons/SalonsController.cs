namespace FeelingSpa.Web.Controllers.Salons
{
    using System;
    using System.Threading.Tasks;

    using FeelingSpa.Services.Data.Categories;
    using FeelingSpa.Services.Data.Salons;
    using FeelingSpa.Web.ViewModels.Salons;
    using Microsoft.AspNetCore.Hosting;
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

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 6;
            var viewModel = new SalonsListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                SalonsCount = this.salonsService.GetCount(),
                Salons = this.salonsService.GetAll<SalonInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult SingleSalon(string id)
        {
            var viewModel = this.salonsService.GetById<SalonWhitServicesViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSalon(string id)
        {
            await this.salonsService.DeleteAsync(id);

            return this.RedirectToAction("/");
        }
    }
}
