using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoxHound.App.Blogs.CreateComment
{
    public class CreateCommentCommand : IRequest<int>
    {
        public int PostId { get; set; } = 0;
        public string Author { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
