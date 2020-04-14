using FoxHound.App.Blogs.Common;
using FoxHound.App.Data;
using FoxHound.App.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoxHound.App.Blogs.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentResult>
    {
        private readonly IFoxHoundData _foxHoundData;

        public CreateCommentCommandHandler(IFoxHoundData foxHoundData)
        {
            _foxHoundData = foxHoundData;
        }

        public async Task<CommentResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            Post post = await _foxHoundData.Posts
                .Include(x => x.Comments)
                .SingleAsync(x => x.PostId == request.PostId, cancellationToken);

            Comment comment = new Comment(request.Author, request.Content);

            post.Comments.Add(comment);
            await _foxHoundData.SaveChangesAsync(cancellationToken);

            return new CommentResult(comment.CommentId, comment.Author, comment.Content, comment.CreatedDate);
        }
    }
}