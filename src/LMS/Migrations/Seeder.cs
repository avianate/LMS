using System;
using System.Collections.Generic;
using System.Linq;
using LMS.Data;
using LMS.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LMS.Migrations
{
    public class Seeder
    {
        private LMSContext _context;
        private UserManager<User> _userManager;

        public Seeder(LMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("instructor@aircraftcheckout.com") == null)
            {
                var newUser = new User
                {
                    UserName = "instructor",
                    Email = "instructor@aircraftcheckout.com"
                };

                await _userManager.CreateAsync(newUser, "P@ssword1");
            }

            if (!_context.Courses.Any())
            {
                var course = new Course
                {
                    Title = "Cessna 172S",
                    Subtitle = "Steam Guage Edition",
                    CourseUrl = "/c172s-steam",
                    Description = "Everything you need to know to about the Cessna 172 SP",
                    IsPublished = true
                };

                _context.Courses.Add(course);
                _context.SaveChanges();
            }

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
                        Comments = new List<Comment> { comment },
                        Author = "instructor"
                    },

                    new Post
                    {
                        Id = 2,
                        Title = "Second Post",
                        Body = "This is a second post with some &lt;strong&gt;HTML&lt;/strong&gt;&lt;em&gt;entities&lt;/em&gt; mixed in.",
                        ImageUrl = "/images/clock.svg",
                        Slug = "second-post",
                        PublishDate = DateTime.UtcNow.AddMinutes(-7),
                        ModifiedDate = DateTime.UtcNow.AddMinutes(-7),
                        IsPublished = true,
                        Categories = new List<Category> { category },
                        Tags = new List<Tag> { tag },
                        Comments = new List<Comment> { comment },
                        Author = "instructor"
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
                        Tags = new List<Tag> { tag2 },
                        Author = "instructor"
                    },

                    new Post
                    {
                        Id = 4,
                        Title = "Fourth Post",
                        Body = "Fourth Body",
                        ImageUrl = "",
                        Slug = "fourth-post",
                        IsPublished = false,
                        ModifiedDate = DateTime.UtcNow.AddMinutes(-3),
                        Author = "instructor"
                    },

                    new Post
                    {
                        Id = 5,
                        Title = "Fifth Post",
                        Body = "Fifth Body",
                        ImageUrl = "",
                        Slug = "fifth-post",
                        IsPublished = false,
                        ModifiedDate = DateTime.UtcNow,
                        Categories = new List<Category> { category },
                        Tags = new List<Tag> { tag },
                        Author = "instructor"
                    }
                };

                _context.Posts.AddRange(posts);
                _context.SaveChanges();
            }
        }
    }
}