namespace FeelingSpa.Web.ViewModels.Salons
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Common;

    public abstract class BaseSalonInputModel
    {
        [Required]
        [MaxLength(GlobalConstants.DataValidations.NameMaxLength)]
        [MinLength(GlobalConstants.DataValidations.NameMinLength)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        [MinLength(GlobalConstants.DataValidations.AddressMinLength)]
        [MaxLength(GlobalConstants.DataValidations.AddressMaxLength)]
        public string Address { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CitiesItems { get; set; }
    }
}
