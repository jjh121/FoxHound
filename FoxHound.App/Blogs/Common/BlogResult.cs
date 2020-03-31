using System;
using System.Collections.Generic;

namespace FoxHound.App.Blogs.Common
{
    public class BlogResult
    {
        public BlogResult(int blogId, string title, string owner, DateTime createdDate, List<PostSummary> postSummaries)
        {
            BlogId = blogId;
            Title = title;
            Owner = owner;
            CreatedDate = createdDate;
            Posts = postSummaries;
        }

        public int BlogId { get; }
        public string Title { get; }
        public string Owner { get; }
        public DateTime CreatedDate { get; }
        public List<PostSummary> Posts { get; }
    }

    public class PostSummary
    {
        public PostSummary(int postId, string title, DateTime createdDate)
        {
            PostId = postId;
            Title = title;
            CreatedDate = createdDate;
        }

        public int PostId { get; }
        public string Title { get; }
        public DateTime CreatedDate { get; }
    }
}