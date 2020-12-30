using System;
using System.Threading.Tasks;
using FeelingSpa.Services.Data.Categories;
using FeelingSpa.Services.Data.Services;
using FeelingSpa.Web.ViewModels.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeelingSpa.Web.Areas.Administration.Controllers
{
    public class ServicesController : AdministrationController
    {
        private readonly IServicesService servicesService;
        private readonly ICategoriesService categoriesService;

        public ServicesController(IServicesService servicesService, ICategoriesService categoriesService)
        {
            this.servicesService = servicesService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Create()
        {
            var viewModel = new ServiceInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            try
            {
                await this.servicesService.CreateAsync(input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            return this.Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteService(int id)
        {
            await this.servicesService.DeleteAsync(id);

            return this.RedirectToAction("SingleSalon", "Salons");
        }
    }
}
