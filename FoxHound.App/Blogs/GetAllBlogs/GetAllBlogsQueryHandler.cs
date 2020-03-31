using FoxHound.App.Data;
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
            List<BlogResult> blogs = await _foxHoundData.Blogs
                .Select(x => new BlogResult(
                    x.BlogId,
                    x.Title,
                    x.Owner,
                    x.CreatedDate))
                .ToListAsync(cancellationToken);

            return blogs;
        }
    }
}