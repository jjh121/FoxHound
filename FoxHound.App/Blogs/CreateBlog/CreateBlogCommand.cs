using MediatR;

namespace FoxHound.App.Blogs.CreateBlog
{
    public class CreateBlogCommand : IRequest<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }
}