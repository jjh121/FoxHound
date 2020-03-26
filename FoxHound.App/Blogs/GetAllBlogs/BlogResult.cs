using System;

namespace FoxHound.App.Blogs.GetAllBlogs
{
    public class BlogResult
    {
        public BlogResult(int blogId, string owner, DateTime createdDate)
        {
            BlogId = blogId;
            Owner = owner;
            CreatedDate = createdDate;
        }

        public int BlogId { get; }
        public string Owner { get; }
        public DateTime CreatedDate { get; }
    }
}