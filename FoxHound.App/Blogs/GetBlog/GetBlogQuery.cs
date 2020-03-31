using FoxHound.App.Blogs.Common;
using MediatR;

namespace FoxHound.App.Blogs.GetBlog
{
    public class GetBlogQuery : IRequest<BlogResult>
    {
        public GetBlogQuery(int blogId)
        {
            BlogId = blogId;
        }

        public int BlogId { get; }
    }
}