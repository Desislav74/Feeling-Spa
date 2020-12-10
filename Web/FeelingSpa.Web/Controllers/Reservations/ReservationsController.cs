
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FeelingSpa.Data.Models;
using FeelingSpa.Services.Data.Reservations;
using FeelingSpa.Services.Data.SalonServices;
using FeelingSpa.Services.DateTimeParser;
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
        private readonly IDateTimeParserService dateTimeParserService;

        public ReservationsController(IReservationsService reservationsService, ISalonServicesService salonServicesService, UserManager<ApplicationUser> userManager, IDateTimeParserService dateTimeParserService)
        {
            this.reservationsService = reservationsService;
            this.salonServicesService = salonServicesService;
            this.userManager = userManager;
            this.dateTimeParserService = dateTimeParserService;
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

            var viewModel = new ReservationInputModel()
            {
                SalonId = salonId,
                ServiceId = serviceId,
            };
            return this.View(viewModel);
        }

        [HttpPost]

        public async Task<IActionResult> MakeReservation(ReservationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("MakeReservation", new { input.SalonId, input.ServiceId });
            }

            DateTime dateTime;
            try
            {
                dateTime = this.dateTimeParserService.ConvertStrings(input.Date, input.Time);
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("MakeReservation", new { input.SalonId, input.ServiceId });
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            //var userId = await this.userManager.GetUserIdAsync(user);

            await this.reservationsService.AddAsync(userId, input.SalonId, input.ServiceId, dateTime);

            return this.RedirectToAction("DetailsReservation");
        }
    }
}