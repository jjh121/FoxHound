using MediatR;

namespace FoxHound.App.Blogs.CreatePost
{
    public class CreatePostCommand : IRequest<int>
    {
        public int BlogId { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}