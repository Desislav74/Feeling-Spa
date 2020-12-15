using System;
using System.Linq;
using AutoMapper;
using FeelingSpa.Data.Models;
using FeelingSpa.Services.Mapping;
using FeelingSpa.Web.ViewModels.Category;

namespace FeelingSpa.Web.ViewModels.BlogPost
{
    public class BlogPostViewModel : IMapFrom<Blog>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Blog, BlogPostViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        "/images/blogPosts/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
