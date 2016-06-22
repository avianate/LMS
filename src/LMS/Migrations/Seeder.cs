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
                var category = new Category
                {
                    Id = 1,
                    CategoryName = "Category"
                };

                var tag = new Tag
                {
                    Id = 1,
                    TagName = "Tag 1"
                };

                var tag2 = new Tag
                {
                    Id = 2,
                    TagName = "Tag 2"
                };

                var comment = new Comment
                {
                    Id = 1,
                    UserName = "user",
                    UserEmail = "user@email.com",
                    Body = "A comment",
                    PublishDate = DateTime.UtcNow,
                    IsApproved = true,

                };

                _context.Categories.Add(category);
                _context.Tags.Add(tag);
                _context.Tags.Add(tag2);

                var postTags = new List<PostTag>
                {
                    new PostTag { PostId = 1, TagId = 1 },
                    new PostTag { PostId = 1, TagId = 2 },
                    new PostTag { PostId = 2, TagId = 1 },
                    new PostTag { PostId = 3, TagId = 2 },
                    new PostTag { PostId = 5, TagId = 1 }
                };

                var postCategories = new List<PostCategory>
                {
                    new PostCategory { PostId = 1, CategoryId = 1 },
                    new PostCategory { PostId = 2, CategoryId = 1 },
                    new PostCategory { PostId = 5, CategoryId = 1 }
                };

                var postComments = new List<PostComment>
                {
                    new PostComment { PostId = 1, CommentId = 1 },
                    new PostComment { PostId = 2, CommentId = 1 }
                };

                _context.PostTags.AddRange(postTags);
                _context.PostCategories.AddRange(postCategories);
                _context.PostComments.AddRange(postComments);

                var posts = new List<Post>
                {
                    new Post
                    {
                        Id = 1,
                        Title = "First Post",
                        Body = "First Body",
                        ImageUrl = "/images/cfi_items.png",
                        Slug = "first-post",
                        PublishDate = DateTime.UtcNow.AddMinutes(-10),
                        ModifiedDate = DateTime.UtcNow.AddMinutes(-10),
                        IsPublished = true,
                        Categories = new List<Category> { category },
                        Tags = new List<Tag> { tag, tag2 },
                        Comments = new List<Comment> { comment }
                    },

                    new Post
                    {
                        Id = 2,
                        Title = "Second Post",
                        Body = "This is a second post with some <strong>HTML</strong><em>entities</em> mixed in.",
                        ImageUrl = "/images/clock.svg",
                        Slug = "second-post",
                        PublishDate = DateTime.UtcNow.AddMinutes(-7),
                        ModifiedDate = DateTime.UtcNow.AddMinutes(-7),
                        IsPublished = true,
                        Categories = new List<Category> { category },
                        Tags = new List<Tag> { tag },
                        Comments = new List<Comment> { comment }
                    },

                    new Post
                    {
                        Id = 3,
                        Title = "Third Post",
                        Body = "Third Body",
                        ImageUrl = "/images/MultiDevicesResponsiveWebMockupsPack.png",
                        Slug = "third-post",
                        PublishDate = DateTime.UtcNow.AddMinutes(-5),
                        ModifiedDate = DateTime.UtcNow.AddMinutes(-5),
                        IsPublished = true,
                        Tags = new List<Tag> { tag2 }
                    },

                    new Post
                    {
                        Id = 4,
                        Title = "Fourth Post",
                        Body = "Fourth Body",
                        ImageUrl = "",
                        Slug = "fourth-post",
                        IsPublished = false,
                        ModifiedDate = DateTime.UtcNow.AddMinutes(-3)
                    },

                    new Post
                    {
                        Id = 5,
                        Title = "Fifth Post",
                        Body = "Fifth Body",
                        ImageUrl = "",
                        IsPublished = false,
                        ModifiedDate = DateTime.UtcNow,
                        Categories = new List<Category> { category },
                        Tags = new List<Tag> { tag }
                    }
                };

                _context.Posts.AddRange(posts);
                _context.SaveChanges();
            }
        }
    }
}