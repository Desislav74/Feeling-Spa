namespace FeelingSpa.Web.ViewModels.Salons
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class CreateSalonInputModel : BaseSalonInputModel
    {
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CitiesItems { get; set; }
    }
}
