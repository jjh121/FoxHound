using FoxHound.App.Data;
using FoxHound.App.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoxHound.App.Blogs.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly IFoxHoundData _foxHoundData;

        public CreateCommentCommandHandler(IFoxHoundData foxHoundData)
        {
            _foxHoundData = foxHoundData;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            Post post = await _foxHoundData.Posts
                .Include(x => x.Comments)
                .SingleAsync(x => x.PostId == request.PostId, cancellationToken);

            Comment newComment = new Comment(request.Author, request.Content);

            post.Comments.Add(newComment);

            await _foxHoundData.SaveChangesAsync(cancellationToken);

            return newComment.CommentId;
        }
    }
}