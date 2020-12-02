namespace FeelingSpa.Web.ViewModels.Salons
{
    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Mapping;

    public class SalonServicesViewModel : IMapFrom<SalonService>
    {
        public string SalonId { get; set; }

        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public string ServiceDescription { get; set; }

        public bool Available { get; set; }
    }
}
