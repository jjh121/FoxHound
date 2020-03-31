using System;

namespace FoxHound.App.Blogs.GetAllBlogs
{
    public class BlogResult
    {
        public BlogResult(int blogId, string title, string owner, DateTime createdDate)
        {
            BlogId = blogId;
            Title = title;
            Owner = owner;
            CreatedDate = createdDate;
        }

        public int BlogId { get; }
        public string Title { get; }
        public string Owner { get; }
        public DateTime CreatedDate { get; }
    }
}