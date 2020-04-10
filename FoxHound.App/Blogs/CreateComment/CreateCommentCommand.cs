using FoxHound.App.Blogs.Common;
using MediatR;

namespace FoxHound.App.Blogs.CreateComment
{
    public class CreateCommentCommand : IRequest<CommentResult>
    {
        public int PostId { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}