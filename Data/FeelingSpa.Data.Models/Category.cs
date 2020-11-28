namespace FeelingSpa.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Salons = new HashSet<Salon>();
            this.Services = new HashSet<Service>();
            this.Images = new HashSet<Image>();
        }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Salon> Salons { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
