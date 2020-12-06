using System.Collections.Generic;

namespace FeelingSpa.Web.ViewModels.Salons
{
    public abstract class BaseSalonInputModel
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CitiesItems { get; set; }
    }
}
