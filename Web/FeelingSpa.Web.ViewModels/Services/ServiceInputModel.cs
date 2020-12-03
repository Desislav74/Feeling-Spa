namespace FeelingSpa.Web.ViewModels.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ServiceInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MinLength(50)]
        public string Description { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }
    }
}
