namespace FeelingSpa.Web.Controllers.Search
{
    using FeelingSpa.Services.Data.Categories;
    using FeelingSpa.Services.Data.Salons;
    using FeelingSpa.Web.ViewModels.Category;
    using FeelingSpa.Web.ViewModels.Salons;
    using FeelingSpa.Web.ViewModels.Search;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly ISalonsService salonsService;

        public SearchController(ICategoriesService categoriesService, ISalonsService salonsService)
        {
            this.categoriesService = categoriesService;
            this.salonsService = salonsService;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchCategoriesListViewModel()
            {
                Categories = this.categoriesService.GetAllSearch<CategoryNameIdViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult List(SearchListInputModel input)
        {
            var viewModel = new ListViewModel
            {
                Salons = this.salonsService
                    .GetByCategories<SalonInListViewModel>(input.Categories),
            };

            return this.View(viewModel);
        }
    }
}
