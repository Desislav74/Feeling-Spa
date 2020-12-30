namespace FeelingSpa.Web.ViewModels.Salons
{
    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Mapping;

    public class CreateEditInputViewModel : BaseSalonInputModel, IMapFrom<Salon>
    {
        public string Id { get; set; }
    }
}
