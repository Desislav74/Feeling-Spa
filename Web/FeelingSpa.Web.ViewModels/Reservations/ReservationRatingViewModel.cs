namespace FeelingSpa.Web.ViewModels.Reservations
{
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Mapping;

    public class ReservationRatingViewModel : IMapFrom<Reservation>
    {
        public string Id { get; set; }

        public string SalonId { get; set; }

        public string SalonName { get; set; }

        public string SalonCategoryName { get; set; }

        public string SalonCityName { get; set; }

        public string SalonAddress { get; set; }

        public string SalonImageUrl { get; set; }

        public bool? IsSalonRatedByTheUser { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Please choose a valid number of stars from 1 to 5.")]
        public int RateValue { get; set; }
    }
}
