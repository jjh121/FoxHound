using FoxHound.App.Data;
using FoxHound.App.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoxHound.App.Blogs.CreateBlog
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, int>
    {
        private readonly IFoxHoundData _foxHoundData;

        public CreateBlogCommandHandler(IFoxHoundData foxHoundData)
        {
            _foxHoundData = foxHoundData;
        }

        public async Task<int> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            Blog blog = new Blog(request.Owner);
            _foxHoundData.Blogs.Add(blog);
            await _foxHoundData.SaveChangesAsync(cancellationToken);

            return blog.BlogId;
        }
    }
}