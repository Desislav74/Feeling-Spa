namespace FeelingSpa.Web.Controllers.Salons
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Data.Categories;
    using FeelingSpa.Services.Data.Cities;
    using FeelingSpa.Services.Data.Salons;
    using FeelingSpa.Services.Data.SalonServices;
    using FeelingSpa.Services.Data.Services;
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

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 7;
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
    }
}
