namespace FeelingSpa.Web.Controllers
{
    using System.Diagnostics;

    using FeelingSpa.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    { 
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return this.View(
        //        new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        //}

        [Route("/Home/Error/404")]
        public IActionResult Error404()
        {
            return this.View();
        }

        //[Route("/Home/Error/{code:int}")]
        //public IActionResult Error(int code)
        //{
        //    // Could handle different codes here
        //    // or just return the default error view
        //    return this.View();
        //}
    }
}
