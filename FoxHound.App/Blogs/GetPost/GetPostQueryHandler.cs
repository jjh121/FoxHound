using FoxHound.App.Blogs.Common;
using FoxHound.App.Data;
using FoxHound.App.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoxHound.App.Blogs.GetPost
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostResult>
    {
        private readonly IFoxHoundData _foxHoundData;

        public GetPostQueryHandler(IFoxHoundData foxHoundData)
        {
            _foxHoundData = foxHoundData;
        }

        public async Task<PostResult> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            // TODO: tests
            Post post = await _foxHoundData.Posts
                .Include(x => x.Comments)
                .SingleOrDefaultAsync(x => x.PostId == request.PostId, cancellationToken);

            return new PostResult(
                post.PostId,
                post.BlogId,
                post.Title,
                post.Content,
                post.CreatedDate,
                post.Comments.Select(x => new CommentResult(x.CommentId, x.Author, x.Content, x.CreatedDate)).ToList());
        }
    }
}