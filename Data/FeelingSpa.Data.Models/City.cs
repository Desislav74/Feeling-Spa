namespace FeelingSpa.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Data.Common.Models;

    public class City : BaseDeletableModel<int>
    {
        public City()
        {
            this.Salons = new HashSet<Salon>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Salon> Salons { get; set; }
    }
}
