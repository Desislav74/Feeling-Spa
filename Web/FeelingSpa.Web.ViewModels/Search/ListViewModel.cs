namespace FeelingSpa.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using FeelingSpa.Web.ViewModels.Salons;

    public class ListViewModel
    {
        public IEnumerable<SalonInListViewModel> Salons { get; set; }
    }
}
