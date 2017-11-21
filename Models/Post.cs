using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace D_of_C_Blog.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime TimeCreated { get; set; }

        public string ShortDescription { get; set; }

        public virtual List<PostTag> PostTags { get; set; }

        public virtual ApplicationUser PostAuthor { get; set; }

        public Post()
        {
            PostTags = new List<PostTag>();
        }
    }
}