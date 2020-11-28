namespace FeelingSpa.Web.ViewModels.City
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCityInputModel
    {
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
