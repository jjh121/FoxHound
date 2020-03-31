using System;
using System.Collections.Generic;

namespace FoxHound.App.Domain
{
    public class Post
    {
        public Post(string title)
        {
            PostId = 0;
            BlogId = 0;
            Title = title;
            Content = string.Empty;
            CreatedDate = DateTime.Now;
            Comments = new List<Comment>();
        }

        public int PostId { get; }
        public int BlogId { get; }
        public string Title { get; }
        public string Content { get; }
        public DateTime CreatedDate { get; }

        public IList<Comment> Comments { get; }
    }
}