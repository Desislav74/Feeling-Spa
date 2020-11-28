namespace FeelingSpa.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Data.Common.Models;

    public class Service : BaseDeletableModel<int>
    {
        public Service()
        {
            this.Salons = new HashSet<SalonService>();
            this.Reservations = new HashSet<Reservation>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<SalonService> Salons { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
