using FeelingSpa.Common;

namespace FeelingSpa.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    public class BaseCategoryInputModel
    {
        [Required]
        [MinLength(GlobalConstants.DataValidations.CategoryNameMinLength)]
        [MaxLength(GlobalConstants.DataValidations.CategoryNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.DataValidations.DescriptionMinLength)]
        public string Description { get; set; }
    }
}
