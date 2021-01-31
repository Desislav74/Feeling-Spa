using FeelingSpa.Common;

namespace FeelingSpa.Web.ViewModels.City
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCityInputModel
    {
        [MinLength(GlobalConstants.DataValidations.CityNameMinLength)]
        [MaxLength(GlobalConstants.DataValidations.CityNameMaxLength)]
        public string Name { get; set; }
    }
}
