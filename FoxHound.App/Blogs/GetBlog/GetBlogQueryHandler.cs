using FoxHound.App.Blogs.Common;
using FoxHound.App.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoxHound.App.Blogs.GetBlog
{
    public class GetBlogQueryHandler : IRequestHandler<GetBlogQuery, BlogResult>
    {
        private readonly IFoxHoundData _foxHoundData;

        public GetBlogQueryHandler(IFoxHoundData foxHoundData)
        {
            _foxHoundData = foxHoundData;
        }

        public async Task<BlogResult> Handle(GetBlogQuery request, CancellationToken cancellationToken)
        {
            var blog = await _foxHoundData.Blogs
                .Include(x => x.Posts)
                .SingleAsync(x => x.BlogId == request.BlogId, cancellationToken);

            return new BlogResult(
                blog.BlogId,
                blog.Title,
                blog.Owner,
                blog.CreatedDate,
                blog.Posts.Select(post => new PostSummary(post.PostId, post.Title, post.CreatedDate)).ToList());
        }
    }
}