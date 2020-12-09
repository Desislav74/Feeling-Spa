using FeelingSpa.Services.Mapping;

namespace FeelingSpa.Web.ViewModels.Categories
{
    public class CreateEditCategoryInputModel : BaseCategoryInputModel, IMapFrom<Data.Models.Category>
    {
        public int Id { get; set; }
    }
}
