namespace FeelingSpa.Web.ViewModels.BlogPost
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Common;
    using Microsoft.AspNetCore.Http;

    public class BlogPostInputModel
    {
        [Required]
        [MaxLength(GlobalConstants.DataValidations.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(GlobalConstants.DataValidations.ContentMinLength)]
        public string Content { get; set; }

        [MaxLength(GlobalConstants.DataValidations.AuthorMaxLength)]
        public string Author { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
