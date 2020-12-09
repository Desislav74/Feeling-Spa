using System.Collections.Generic;
using FeelingSpa.Data.Models;
using FeelingSpa.Services.Data.SalonServices;
using FeelingSpa.Services.Data.Services;

namespace FeelingSpa.Web.Controllers.Salons
{
    using System;
    using System.Threading.Tasks;

    using FeelingSpa.Services.Data.Categories;
    using FeelingSpa.Services.Data.Cities;
    using FeelingSpa.Services.Data.Salons;
    using FeelingSpa.Web.ViewModels.Salons;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class SalonsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly ISalonsService salonsService;
        private readonly IWebHostEnvironment environment;
        private readonly ICitiesService citiesService;
        private readonly IServicesService servicesService;
        private readonly ISalonServicesService salonServicesService;

        public SalonsController(ICategoriesService categoriesService, ISalonsService salonsService, IWebHostEnvironment environment, ICitiesService citiesService, IServicesService servicesService, ISalonServicesService salonServicesService)
        {
            this.categoriesService = categoriesService;
            this.salonsService = salonsService;
            this.environment = environment;
            this.citiesService = citiesService;
            this.servicesService = servicesService;
            this.salonServicesService = salonServicesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateSalonInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            viewModel.CitiesItems = this.citiesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSalonInputModel input, string image)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                input.CitiesItems = this.citiesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            try
            {
                var salonId = await this.salonsService.CreateAsync(input, $"{this.environment.WebRootPath}/images");
                var servicesIds = await this.servicesService.GetAllIdsByCategoryAsync(input.CategoryId);
                await this.salonServicesService.CreateAsync(salonId, servicesIds);
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

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Edit(string id)
        {
            var inputModel = this.salonsService.GetById<CreateEditInputViewModel>(id);
            inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            inputModel.CitiesItems = this.citiesService.GetAllAsKeyValuePairs();
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CreateEditInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                input.CitiesItems = this.citiesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            await this.salonsService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.SingleSalon), new { id });
        }

    }
}
