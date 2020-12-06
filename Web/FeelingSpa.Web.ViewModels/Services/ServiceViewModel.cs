using FeelingSpa.Data.Models;
using FeelingSpa.Services.Mapping;

namespace FeelingSpa.Web.ViewModels.Services
{
    public class ServiceViewModel : IMapFrom<Service>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public int SalonsCount { get; set; }

        public int AppointmentsCount { get; set; }
    }
}
