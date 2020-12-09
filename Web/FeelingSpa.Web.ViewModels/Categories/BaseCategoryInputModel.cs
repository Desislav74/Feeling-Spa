using System.ComponentModel.DataAnnotations;

namespace FeelingSpa.Web.ViewModels.Categories
{
    public class BaseCategoryInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MinLength(100)]
        public string Description { get; set; }
    }
}
