using System.Collections.Generic;

namespace FeelingSpa.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Data.Common.Models;

    public class Blog : BaseDeletableModel<int>
    {
        public Blog()
        {
            this.Images = new HashSet<Image>();
        }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        // Blog can be created only in the Admin Dashboard
        // So the Author is not a User, just a string for name
        [Required]
        [MaxLength(30)]
        public string Author { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
