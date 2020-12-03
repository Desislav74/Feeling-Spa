namespace FeelingSpa.Web.ViewModels.Category
{
    using System.Collections.Generic;

    public class CategoriesListViewModel : PagingViewModel
    {
        public IEnumerable<CategoryInListViewModel> Categories { get; set; }
    }
}
