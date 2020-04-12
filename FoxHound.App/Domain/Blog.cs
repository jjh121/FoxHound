using System;
using System.Collections.Generic;

namespace FoxHound.App.Domain
{
    public class Blog
    {
        public Blog(string title, string owner)
        {
            BlogId = 0;
            Title = title;
            Owner = owner;
            CreatedDate = DateTime.Now;
            Posts = new List<Post>();
        }

        public int BlogId { get; }
        public string Title { get; private set; }
        public string Owner { get; private set; }
        public DateTime CreatedDate { get; }
        public IList<Post> Posts { get; }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetOwner(string owner)
        {
            Owner = owner;
        }
    }
}