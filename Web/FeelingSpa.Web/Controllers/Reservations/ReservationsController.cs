namespace FeelingSpa.Web.Controllers.Reservations
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Data.Reservations;
    using FeelingSpa.Services.Data.Salons;
    using FeelingSpa.Services.Data.SalonServices;
    using FeelingSpa.Services.DateTimeParser;
    using FeelingSpa.Web.ViewModels.Reservations;
    using FeelingSpa.Web.ViewModels.SalonServices;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;

    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly IReservationsService reservationsService;
        private readonly ISalonServicesService salonServicesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDateTimeParserService dateTimeParserService;
        private readonly ISalonsService salonsService;

        public ReservationsController(IReservationsService reservationsService, ISalonServicesService salonServicesService, UserManager<ApplicationUser> userManager, IDateTimeParserService dateTimeParserService, ISalonsService salonsService)
        {
            this.reservationsService = reservationsService;
            this.salonServicesService = salonServicesService;
            this.userManager = userManager;
            this.dateTimeParserService = dateTimeParserService;
            this.salonsService = salonsService;
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

            await this.reservationsService.AddAsync(userId, input.SalonId, input.ServiceId, dateTime);

            return this.RedirectToAction("DetailsReservation");
        }

        [HttpGet]
        public async Task<IActionResult> CancelReservation(string id)
        {
            var viewModel = await this.reservationsService.GetByIdAsync<ReservationViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReservation(string id)
        {
            await this.reservationsService.DeleteAsync(id);

            return this.RedirectToAction("DetailsReservation");
        }

        public async Task<IActionResult> RatePastReservation(string id)
        {
            var viewModel = await this.reservationsService.GetByIdAsync<ReservationRatingViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RateSalon(ReservationRatingViewModel rating)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("RatePastReservation", new { id = rating.Id });
            }

            if (rating.IsSalonRatedByUser == true)
            {
                return this.RedirectToAction("RatePastReservation", new { id = rating.Id });
            }

            await this.reservationsService.RateReservationAsync(rating.Id);
            await this.salonsService.RateSalonAsync(rating.SalonId, rating.RateValue);

            return this.RedirectToAction("SingleSalon", "Salons", new { id = rating.SalonId });
        }
    }
}
