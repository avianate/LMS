using System;
using System.Linq;
using LMS.Data;
using LMS.Entities;

namespace LMS.Migrations
{
    public class Seeder
    {
        private LMSContext _context;

        public Seeder(LMSContext context) 
        {
            _context = context;
        }

        public void EnsureSeedData()
        {
            if (!_context.Posts.Any())
            {
                var post = new Post()
                {
                      Title = "First Post",
                      Body = "Body Content",
                      ImageUrl = "",
                      Slug = "first-post",
                      PublishDate = DateTime.UtcNow,
                      IsPublished = true,
                      ModifiedDate = DateTime.UtcNow
                };

                _context.Posts.Add(post);
                
                var tag = new Tag()
                {
                    TagName = "Tag"
                };

                _context.Tags.Add(tag);

                var category = new Category()
                {
                    CategoryName = "Category"
                };

                _context.Categories.Add(category);

                var comment = new Comment()
                {
                    Body = "Comment Body",
                    PublishDate = DateTime.UtcNow,
                    IsApproved = true,
                    UserEmail = "a@b.com",
                    UserName = "username"
                };

                _context.Comments.Add(comment);
                _context.SaveChanges();
            }
        }
    }
}