using FeelingSpa.Common;

namespace FeelingSpa.Web.ViewModels.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ServiceInputModel
    {
        [Required]
        [MinLength(GlobalConstants.DataValidations.ServiceNameMinLength)]
        [MaxLength(GlobalConstants.DataValidations.ServiceNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MinLength(GlobalConstants.DataValidations.DescriptionMinLength)]
        public string Description { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }
    }
}
