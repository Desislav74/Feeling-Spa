namespace FeelingSpa.Web.ViewModels.Category
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Http;

    public class CreateCategoryInputModel : BaseCategoryInputModel
    {
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
