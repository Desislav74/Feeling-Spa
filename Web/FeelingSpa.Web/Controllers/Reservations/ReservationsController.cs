using System.Threading.Tasks;
using FeelingSpa.Services.Data.Reservations;
using FeelingSpa.Services.Data.SalonServices;
using FeelingSpa.Web.ViewModels.Reservations;
using Microsoft.AspNetCore.Mvc;

namespace FeelingSpa.Web.Controllers.Reservations
{
    public class ReservationsController
    {
        private readonly IReservationsService reservationsService;
        private readonly ISalonServicesService salonServicesService;


        public ReservationsController(IReservationsService reservationsService, ISalonServicesService salonServicesService)
        {
            this.reservationsService = reservationsService;
            this.salonServicesService = salonServicesService;
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            var userId = await this.userManager.GetUserIdAsync(user);

            var viewModel = new ReservationsListViewModel()
            {
                Reservations =
                    await this.reservationsService.GetUpcomingByUserAsync<ReservationViewModel>(userId),
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> MakeReservation(string salonId, int serviceId)
        {
            var salonService = await this.salonServicesService.GetByIdAsync<SalonServiceSimpleViewModel>(salonId, serviceId);
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
