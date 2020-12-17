namespace FeelingSpa.Web.ViewModels.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public class ItemsList<T> : List<T>
    {
        public ItemsList(List<T> items, int count)
        {
            this.AddRange(items);
        }
    }
}
