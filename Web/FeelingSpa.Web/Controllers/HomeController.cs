using FeelingSpa.Services.Data.Salons;
using FeelingSpa.Web.ViewModels.Home;
using FeelingSpa.Web.ViewModels.Home;
using IndexViewModel = FeelingSpa.Web.ViewModels.Administration.Dashboard.IndexViewModel;

namespace FeelingSpa.Web.Controllers
{
    using System.Diagnostics;

    using FeelingSpa.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ISalonsService salonsService;

        public HomeController(ISalonsService salonsService)
        {
            this.salonsService = salonsService;
        }

        public IActionResult Index()
        {
            var viewModel = new SalonsIndexViewModel
            {
                RandomSalons = this.salonsService.GetRandom<HomePageSalonsViewModel>(10),
            };
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
