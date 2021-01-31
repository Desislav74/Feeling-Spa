namespace FeelingSpa.Web.ViewModels.Reservations
{
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Common;
    using FeelingSpa.Web.ViewModels.Common;

    public class ReservationInputModel
    {
        [Required]
        public string SalonId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        [ValidDate(ErrorMessage = GlobalConstants.ErrorMessages.DateTime)]
        public string Date { get; set; }

        [Required]
        [ValidTime(ErrorMessage = GlobalConstants.ErrorMessages.DateTime)]
        public string Time { get; set; }
    }
}
