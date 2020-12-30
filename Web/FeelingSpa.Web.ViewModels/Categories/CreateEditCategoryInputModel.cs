namespace FeelingSpa.Web.ViewModels.Categories
{
    using FeelingSpa.Services.Mapping;

    public class CreateEditCategoryInputModel : BaseCategoryInputModel, IMapFrom<Data.Models.Category>
    {
        public int Id { get; set; }
    }
}
