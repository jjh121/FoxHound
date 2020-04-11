using FoxHound.App.Blogs.Common;
using MediatR;

namespace FoxHound.App.Blogs.UpdateBlog
{
    public class UpdateBlogCommand : IRequest, IBlogCommand
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }
}