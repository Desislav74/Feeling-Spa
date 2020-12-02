namespace FeelingSpa.Web.ViewModels.Salons
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Mapping;

    public class SalonWhitServicesViewModel : IMapFrom<Salon>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CityName { get; set; }

        public string Address { get; set; }

        public double Rating { get; set; }

        public int RatingCount { get; set; }

        public virtual ICollection<SalonServicesViewModel> Services { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Salon, SalonWhitServicesViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        "/images/salons/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
