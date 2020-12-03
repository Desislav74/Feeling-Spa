namespace FeelingSpa.Web.ViewModels.Reservations
{
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Web.ViewModels.Common;

    public class ReservationInputModel
    {
        [Required]
        public string SalonId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        [ValidDate(ErrorMessage = "Please select valid data and time")]
        public string Date { get; set; }

        [Required]
        [ValidTime(ErrorMessage = "Please select valid data and time")]
        public string Time { get; set; }
    }
}
