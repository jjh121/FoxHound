using FoxHound.App.Blogs.Common;
using MediatR;

namespace FoxHound.App.Blogs.GetPost
{
    public class GetPostQuery : IRequest<PostResult>
    {
        public GetPostQuery(int postId)
        {
            PostId = postId;
        }

        public int PostId { get; }
    }
}