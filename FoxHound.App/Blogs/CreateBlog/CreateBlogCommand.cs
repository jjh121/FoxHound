using FoxHound.App.Blogs.Common;
using MediatR;

namespace FoxHound.App.Blogs.CreateBlog
{
    public class CreateBlogCommand : IRequest<int>, IBlogCommand
    {
        public string Title { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }
}