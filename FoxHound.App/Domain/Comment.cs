using System;

namespace FoxHound.App.Domain
{
    public class Comment
    {
        public Comment(string author, string content)
        {
            CommentId = 0;
            PostId = 0;
            Author = author;
            Content = content;
            CreatedDate = DateTime.Now;
        }

        public int CommentId { get; }
        public int PostId { get; }
        public string Author { get; }
        public string Content { get; }
        public DateTime CreatedDate { get; }
    }
}