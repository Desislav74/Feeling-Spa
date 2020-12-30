namespace FeelingSpa.Web.ViewModels.Salons
{
    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Mapping;

    public class SalonSimpleViewModel : IMapFrom<Salon>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
