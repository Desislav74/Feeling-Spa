namespace FeelingSpa.Web.ViewModels.SalonServices
{
    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Mapping;

    public class SalonServicesSimpleViewModel : IMapFrom<SalonService>
    {
        public string SalonId { get; set; }

        public int ServiceId { get; set; }

        public bool Available { get; set; }
    }
}
