﻿namespace FeelingSpa.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Data.Common.Models;

    public class SalonService : BaseDeletableModel<int>
    {
        public SalonService()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        [Required]
        public string SalonId { get; set; }

        public virtual Salon Salon { get; set; }

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }

        // Each Salon gets all Services from its Category, but may not provide them all
        public bool Available { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
