namespace FeelingSpa.Web.ViewModels.Salons
{
    using System.Collections.Generic;

    public class SalonsListViewModel : PagingViewModel
    {
        public IEnumerable<SalonInListViewModel> Salons { get; set; }
    }
}
