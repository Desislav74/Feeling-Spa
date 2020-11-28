using FeelingSpa.Data.Common.Models;

namespace FeelingSpa.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Salon : BaseDeletableModel<string>
    {
        public Salon()
        {
            this.Reservations = new HashSet<Reservation>();
            this.Services = new HashSet<SalonService>();
            this.Images = new HashSet<Image>();
        }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public string StoreOwnerId { get; set; }

        public ApplicationUser StoreOwner { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public double Rating { get; set; }

        public int RatingCount { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<SalonService> Services { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
