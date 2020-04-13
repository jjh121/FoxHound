using System;

namespace FoxHound.App.Blogs.Common
{
    public class CommentResult
    {
        public CommentResult(int commentId, string author, string content, DateTime createdDate)
        {
            CommentId = commentId;
            Author = author;
            Content = content;
            CreatedDate = createdDate;
        }

        public int CommentId { get; }
        public string Author { get; }
        public string Content { get; }
        public DateTime CreatedDate { get; }
    }
}