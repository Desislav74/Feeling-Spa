
using System.Security.Claims;
using System.Threading.Tasks;
using FeelingSpa.Data.Models;
using FeelingSpa.Services.Data.Reservations;
using FeelingSpa.Services.Data.SalonServices;
using FeelingSpa.Web.ViewModels.Reservations;
using FeelingSpa.Web.ViewModels.SalonServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FeelingSpa.Web.Controllers.Reservations
{
    public class ReservationsController : Controller
    {
        private readonly IReservationsService reservationsService;
        private readonly ISalonServicesService salonServicesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReservationsController(IReservationsService reservationsService, ISalonServicesService salonServicesService, UserManager<ApplicationUser> userManager)
        {
            this.reservationsService = reservationsService;
            this.salonServicesService = salonServicesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> DetailsReservation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = new ReservationsListViewModel
            {
                Reservations =
                    await this.reservationsService.GetReservationsByUserAsync<ReservationViewModel>(userId),
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> MakeReservation(string salonId, int serviceId)
        {
            var salonService =
                await this.salonServicesService.GetByIdAsync<SalonServicesSimpleViewModel>(salonId, serviceId);
            if (salonService == null || !salonService.Available)
            {
                return this.View("UnavailableService");
            }

            var viewModel = new ReservationInputModel()
            {
                SalonId = salonId,
                ServiceId = serviceId,
            };
            return this.View(viewModel);
        }
    }
}