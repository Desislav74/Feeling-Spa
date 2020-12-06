using FeelingSpa.Data.Models;
using FeelingSpa.Services.Mapping;

namespace FeelingSpa.Web.ViewModels.Salons
{
    public class CreateEditInputViewModel : BaseSalonInputModel, IMapFrom<Salon>
    {
        public string Id { get; set; }
    }
}
