namespace FeelingSpa.Web.ViewModels.BlogPost
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Common;
    using Microsoft.AspNetCore.Http;

    public class BlogPostInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string Author { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
