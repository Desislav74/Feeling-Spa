namespace FeelingSpa.Web.ViewModels.Common
{
    using System.Collections.Generic;

    public class ItemsList<T> : List<T>
    {
        public ItemsList(List<T> items)
        {
            this.AddRange(items);
        }
    }
}
