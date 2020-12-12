namespace FeelingSpa.Web.ViewModels.Home
{
    using System.Linq;

    using AutoMapper;
    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Mapping;

    public class HomePageSalonsViewModel : IMapFrom<Salon>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Salon, HomePageSalonsViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        "/images/salons/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
