using FoxHound.App.Data;
using FoxHound.App.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoxHound.App.Blogs.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IFoxHoundData _foxHoundData;

        public CreatePostCommandHandler(IFoxHoundData foxHoundData)
        {
            _foxHoundData = foxHoundData;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            Blog blog = await _foxHoundData.Blogs
                .Include(x => x.Posts)
                .SingleAsync(x => x.BlogId == request.BlogId, cancellationToken);

            Post newPost = new Post(request.Title, request.Content);

            blog.Posts.Add(newPost);

            await _foxHoundData.SaveChangesAsync(cancellationToken);

            return newPost.PostId;
        }
    }
}