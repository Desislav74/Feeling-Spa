namespace FeelingSpa.Web.Areas.Manager.Controllers
{
    using System.Threading.Tasks;

    using FeelingSpa.Services.Data.Reservations;
    using FeelingSpa.Services.Data.Salons;
    using FeelingSpa.Services.Data.SalonServices;
    using FeelingSpa.Web.ViewModels.Salons;
    using Microsoft.AspNetCore.Mvc;

    public class SalonsController : ManagerBaseController
    {
        private readonly ISalonsService salonsService;
        private readonly ISalonServicesService salonServicesService;
        private readonly IReservationsService reservationsService;

        public SalonsController(
            ISalonsService salonsService,
            ISalonServicesService salonServicesService,
            IReservationsService reservationsService)
        {
            this.salonsService = salonsService;
            this.salonServicesService = salonServicesService;
            this.reservationsService = reservationsService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.salonsService.GetByIdAsync<SalonWhitServicesViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeServiceAvailableStatus(string salonId, int serviceId)
        {
            await this.salonServicesService.ChangeAvailableStatusAsync(salonId, serviceId);

            return this.RedirectToAction("Details", new { id = salonId });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmReservation(string id, string salonId)
        {
            await this.reservationsService.ConfirmAsync(id);
            return this.RedirectToAction("Details", new { id = salonId });
        }

        [HttpPost]
        public async Task<IActionResult> DeclineReservation(string id, string salonId)
        {
            await this.reservationsService.DeclineAsync(id);
            return this.RedirectToAction("Details", new { id = salonId });
        }
    }
}
