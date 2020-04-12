using System;
using System.Collections.Generic;

namespace FoxHound.App.Blogs.Common
{
    public class PostResult
    {
        public PostResult(int postId, int blogId, string title, string content, DateTime createdDate, List<CommentResult> commentSummaries)
        {
            PostId = postId;
            BlogId = blogId;
            Title = title;
            Content = content;
            CreatedDate = createdDate;
            Comments = commentSummaries;
        }

        public int PostId { get; }
        public int BlogId { get; }
        public string Title { get; }
        public string Content { get; }
        public DateTime CreatedDate { get; }
        public List<CommentResult> Comments { get; }
    }

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