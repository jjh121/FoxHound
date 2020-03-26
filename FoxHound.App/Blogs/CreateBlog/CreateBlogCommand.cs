using MediatR;

namespace FoxHound.App.Blogs.CreateBlog
{
    public class CreateBlogCommand : IRequest<int>
    {
        public string Owner { get; set; } = string.Empty;
    }
}