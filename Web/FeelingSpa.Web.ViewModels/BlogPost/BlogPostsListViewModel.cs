using System.Collections.Generic;

namespace FeelingSpa.Web.ViewModels.BlogPost
{
    public class BlogPostsListViewModel : PagingViewModel
    {
        public IEnumerable<BlogPostViewModel> BlogPosts { get; set; }
    }
}
