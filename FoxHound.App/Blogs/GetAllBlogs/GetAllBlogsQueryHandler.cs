using FoxHound.App.Blogs.Common;
using FoxHound.App.Data;
using FoxHound.App.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoxHound.App.Blogs.GetAllBlogs
{
    public class GetAllBlogsQueryHandler : IRequestHandler<GetAllBlogsQuery, List<BlogResult>>
    {
        private readonly IFoxHoundData _foxHoundData;

        public GetAllBlogsQueryHandler(IFoxHoundData foxHoundData)
        {
            _foxHoundData = foxHoundData;
        }

        public async Task<List<BlogResult>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
        {
            List<Blog> blogs = await _foxHoundData.Blogs
                    .Include(x => x.Posts)
                    .ToListAsync(cancellationToken);

            List<BlogResult> blogResults = blogs
                .Select(x => new BlogResult(
                    x.BlogId,
                    x.Title,
                    x.Owner,
                    x.CreatedDate,
                    x.Posts.Select(post => new PostSummary(post.PostId, post.Title, post.CreatedDate)).ToList()))
                .ToList();

            return blogResults;
        }
    }
}