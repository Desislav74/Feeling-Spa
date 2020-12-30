namespace FeelingSpa.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using FeelingSpa.Services.Data.Reservations;
    using FeelingSpa.Web.ViewModels.Reservations;
    using Microsoft.AspNetCore.Mvc;

    public class AllReservationsBySalonViewComponent : ViewComponent
    {
        private readonly IReservationsService reservationsService;

        public AllReservationsBySalonViewComponent(IReservationsService reservationsService)
        {
            this.reservationsService = reservationsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string salonId)
        {
            var viewModel = new ReservationsListViewModel
            {
                Reservations = 
                    await this.reservationsService.GetAllBySalonAsync<ReservationViewModel>(salonId),
            };

            return this.View(viewModel);
        }
    }
}
