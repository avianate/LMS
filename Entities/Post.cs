using System;
using System.Collections.Generic;

namespace LMS.Entities
{
    public class Post
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string Body { get; set; }

        public string ImageUrl { get; set; }

        public string Slug { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsPublished { get; set; }

        public DateTime ModifiedDate { get; set; }

        

        // Navigation properties
        public virtual ICollection<PostTag> PostTags { get; set; }

        public virtual ICollection<PostCategory> PostCategories { get; set; }

        public virtual ICollection<PostComment> PostComments { get; set; }
    }
}