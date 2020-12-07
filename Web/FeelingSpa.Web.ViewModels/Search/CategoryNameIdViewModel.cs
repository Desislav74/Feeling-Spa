namespace FeelingSpa.Web.ViewModels.Search
{
    using FeelingSpa.Services.Mapping;

    public class CategoryNameIdViewModel : IMapFrom<Data.Models.Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
