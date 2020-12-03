namespace FeelingSpa.Web.ViewModels.Category
{
    using System.Linq;

    using AutoMapper;
    using FeelingSpa.Services.Mapping;

    public class CategoryInListViewModel : IMapFrom<Data.Models.Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Category, CategoryInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        "/images/categories/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
