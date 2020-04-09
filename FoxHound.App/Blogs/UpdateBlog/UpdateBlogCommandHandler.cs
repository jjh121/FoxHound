using FoxHound.App.Data;
using FoxHound.App.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoxHound.App.Blogs.UpdateBlog
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, Unit>
    {
        private readonly IFoxHoundData _foxHoundData;

        public UpdateBlogCommandHandler(IFoxHoundData foxHoundData)
        {
            _foxHoundData = foxHoundData;
        }

        public async Task<Unit> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            Blog blog = await _foxHoundData.Blogs
                .SingleAsync(x => x.BlogId == request.BlogId, cancellationToken);

            blog.SetOwner(request.Owner);
            blog.SetTitle(request.Title);
            await _foxHoundData.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}