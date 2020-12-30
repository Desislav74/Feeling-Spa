namespace FeelingSpa.Web.Areas.Administration.Controllers
{
    using FeelingSpa.Services.Data;
    using FeelingSpa.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
