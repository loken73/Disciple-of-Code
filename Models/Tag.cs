using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D_of_C_Blog.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        public string Name { get; set; }

        public virtual List<PostTag> PostTags { get; set; }


        public Tag()
        {
            PostTags = new List<PostTag>();
        }
    }
}
