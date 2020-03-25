using System;
using System.Collections.Generic;

namespace FoxHound.App.Domain
{
    public class Blog
    {
        public Blog(string owner)
        {
            BlogId = 0;
            Owner = owner;
            CreatedDate = DateTime.Now;
            Posts = new List<Post>();
        }

        public int BlogId { get; }
        public string Owner { get; }
        public DateTime CreatedDate { get; }

        public IList<Post> Posts { get; }
    }
}