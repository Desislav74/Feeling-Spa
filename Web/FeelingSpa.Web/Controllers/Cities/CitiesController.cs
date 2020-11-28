namespace FeelingSpa.Web.Controllers.Cities
{
    using System.Threading.Tasks;

    using FeelingSpa.Services.Data.Cities;
    using FeelingSpa.Web.ViewModels.City;
    using Microsoft.AspNetCore.Mvc;

    public class CitiesController : Controller
    {
        private readonly ICitiesService citiesService;

        public CitiesController(ICitiesService citiesService)
        {
            this.citiesService = citiesService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCityInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.citiesService.CreateAsync(input);
            return this.Redirect("/");
        }
    }
}
