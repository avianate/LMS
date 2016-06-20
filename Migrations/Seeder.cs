using System;
using System.Collections.Generic;
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
                var postTags = new List<PostTag>
                {
                    new PostTag { PostId = 1, TagId = 1 }
                };

                var postCategories = new List<PostCategory>
                {
                    new PostCategory { PostId = 1, CategoryId = 1 }
                };

                var postComments = new List<PostComment> 
                {
                    new PostComment { PostId = 1, CommentId = 1 }
                };

                var post = new Post()
                {
                      Title = "First Post",
                      Body = "Body Content",
                      ImageUrl = "",
                      Slug = "first-post",
                      PublishDate = DateTime.UtcNow,
                      IsPublished = true,
                      ModifiedDate = DateTime.UtcNow,
                      PostTags = postTags,
                      PostCategories = postCategories,
                      PostComments = postComments
                };

                _context.Posts.Add(post);
                
                var tag = new Tag()
                {
                    TagName = "Tag",
                    PostTags = postTags
                };

                _context.Tags.Add(tag);

                var category = new Category()
                {
                    CategoryName = "Category",
                    PostCategories = postCategories
                };

                _context.Categories.Add(category);

                var comment = new Comment()
                {
                    Body = "Comment Body",
                    PublishDate = DateTime.UtcNow,
                    IsApproved = true,
                    UserEmail = "a@b.com",
                    UserName = "username",
                    PostComments = postComments
                };

                _context.Comments.Add(comment);
                _context.SaveChanges();
            }
        }
    }
}