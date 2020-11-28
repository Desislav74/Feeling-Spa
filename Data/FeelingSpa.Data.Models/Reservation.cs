namespace FeelingSpa.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Data.Common.Models;

    public class Reservation : BaseDeletableModel<string>
    {
        public DateTime DateTime { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string SalonId { get; set; }

        public virtual Salon Salon { get; set; }

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }

        public virtual SalonService SalonService { get; set; }

        // The Salon can Confirm or Decline an reservation
        public bool? Confirmed { get; set; }

        // For every past (and confirmed) reservation the User can Rate the Salon
        // But rating can be given only once for each reservation
        public bool? IsSalonRatedByUser { get; set; }
    }
}
