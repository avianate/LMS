using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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



        [NotMapped]
        public List<Tag> Tags { get; set; }

        [NotMapped]
        public List<Category> Categories { get; set; }

        [NotMapped]
        public List<Comment> Comments { get; set; }
    }
}