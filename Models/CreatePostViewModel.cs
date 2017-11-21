using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace D_of_C_Blog.Models
{
    public class CreatePostViewModel
    {
        [Required]
        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }

        [Required]
        [Display(Name = "Post Description")]
        public string PostShortDescription { get; set; }
        
        [Required]
        [Display(Name = "Post Body")]
        public string PostBody { get; set; }
        
        [Required]
        public string Tags { get; set;}
    }
}