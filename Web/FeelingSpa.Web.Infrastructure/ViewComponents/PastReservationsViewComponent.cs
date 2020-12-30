namespace FeelingSpa.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Data.Reservations;
    using FeelingSpa.Web.ViewModels.Reservations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PastReservationsViewComponent : ViewComponent
    {
        private readonly IReservationsService reservationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PastReservationsViewComponent(
            IReservationsService reservationsService,
            UserManager<ApplicationUser> userManager)
        {
            this.reservationsService = reservationsService;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
                var user = await this.userManager.GetUserAsync(this.HttpContext.User);
                var userId = await this.userManager.GetUserIdAsync(user);

                var viewModel = new ReservationsListViewModel()
                {
                    Reservations = 
                        await this.reservationsService.GetPastByUserAsync<ReservationViewModel>(userId),
                };

                return this.View(viewModel);
        }
    }
}

