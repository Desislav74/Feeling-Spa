namespace FeelingSpa.Web.ViewModels.BlogPost
{
    using System.Collections.Generic;

    public class BlogPostsListViewModel : PagingViewModel
    {
        public IEnumerable<BlogPostViewModel> BlogPosts { get; set; }
    }
}
