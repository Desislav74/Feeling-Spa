namespace FeelingSpa.Web.ViewModels.Salons
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class CreateSalonInputModel
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }
    }
}
