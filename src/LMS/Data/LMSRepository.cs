using System;
using System.Collections.Generic;
using System.Linq;
using LMS.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    /// <summary>
    ///     The LMS Repository
    /// </summary>
    public class LMSRepository : ILMSRepository
    {
        #region Fields

        private LMSContext _context;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Constructor with injected context
        /// </summary>
        /// <param name="context"></param>
        public LMSRepository(LMSContext context)
        {
            _context = context;
        }

        #endregion Constructors

        #region Methods

        public Post GetPost(string slug)
        {
            var post = _context.Posts
                                    .Where(s => s.Slug == slug)
                                    .OrderBy(d => d.PublishDate)
                                    .FirstOrDefault();

            return post;
        }

        public Post GetPublishedPost(string slug)
        {
            var post = GetPost(slug);

            if (post.IsPublished)
            {
                return post;
            }

            return null;
        }

        /// <summary>
        ///     Gets list of all categories
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets all categories for a specific post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public IEnumerable<Category> GetAllCategoriesForPost(int postId)
        {
            var postCategories = _context.PostCategories;
            var categories = _context.Categories;

            var categoryIds = postCategories.Where(a => a.PostId == postId).Select(b => b.CategoryId).ToList();
            var categoriesForPost = new List<Category>();

            foreach (var categoryId in categoryIds)
            {
                var category = categories.Where(a => a.Id == categoryId);
                categoriesForPost.AddRange(category);
            }

            return categoriesForPost;
        }

        /// <summary>
        ///     Gets all comments for a specific post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public IEnumerable<Comment> GetAllCommentsForPost(int postId)
        {
            var postComments = _context.PostComments;
            var comments = _context.Comments;

            var commentIds = postComments.Where(a => a.PostId == postId).Select(b => b.CommentId).ToList();
            var commentsForPost = new List<Comment>();

            foreach (var commentId in commentIds)
            {
                var comment = comments.Where(a => a.Id == commentId);
                commentsForPost.AddRange(comment);
            }

            return commentsForPost;
        }

        /// <summary>
        ///     Gets all posts whether published or not
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Post> GetAllPosts()
        {
            var posts = _context.Posts
                                    .OrderByDescending(d => d.ModifiedDate)
                                    .ToList();

            SetAllPostEntities(posts);

            return posts;
        }

        /// <summary>
        ///     Gets all published posts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Post> GetAllPublishedPosts()
        {
            var postTags = _context.PostTags;
            var posts = _context.Posts
                                    .Where(p => p.IsPublished)
                                    .OrderBy(t => t.Title)
                                    .ToList();

            SetAllPostEntities(posts);

            return posts;
        }

        /// <summary>
        ///     Gets all the tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tag> GetAllTags()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets all tags for a specific post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public IEnumerable<Tag> GetAllTagsForPost(int postId)
        {
            var postTags = _context.PostTags;
            var tags = _context.Tags;

            var tagIds = postTags.Where(a => a.PostId == postId).Select(b => b.TagId).ToList();
            var tagsForPost = new List<Tag>();

            foreach (var tagId in tagIds)
            {
                var tag = tags.Where(a => a.Id == tagId);
                tagsForPost.AddRange(tag);
            }

            return tagsForPost;
        }

        /// <summary>
        ///     Gets all posts for a specific author / user
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetUserPosts(string name)
        {
            var posts = _context.Posts
                                    .Where(n => n.Author == name)
                                    .OrderBy(d => d.PublishDate)
                                    .ToList();

            return posts;
        }

        /// <summary>
        ///     Adds the tags, categories, and comments to a post after
        ///     it is retrieved from the database
        /// </summary>
        /// <param name="posts"></param>
        private void SetAllPostEntities(List<Post> posts)
        {
            foreach (var post in posts)
            {
                var id = post.Id;

                var tags = GetAllTagsForPost(id);
                var categories = GetAllCategoriesForPost(id);
                var comments = GetAllCommentsForPost(id);

                InitPostProperties(post);

                post.Tags.AddRange(tags);
                post.Categories.AddRange(categories);
                post.Comments.AddRange(comments);
            }
        }

        /// <summary>
        ///     Initializes tags, categories, and comments navigation properties 
        ///     on each post when it is retrieved from the database
        /// </summary>
        /// <param name="post"></param>
        private void InitPostProperties (Post post)
        {
            if (post.Tags == null)
            {
                post.Tags = new List<Tag>();
            }

            if (post.Categories == null)
            {
                post.Categories = new List<Category>();
            }

            if (post.Comments == null)
            {
                post.Comments = new List<Comment>();
            }
        }

        public IEnumerable<Course> GetAllCourses()
        {
            var courses = _context.Courses
                                    .OrderBy(t => t.Title)
                                    .ToList();

            return courses;
        }

        public IEnumerable<Course> GetAllPublishedCourses()
        {
            var courses = _context.Courses
                                    .Where(p => p.IsPublished)
                                    .OrderBy(t => t.Title)
                                    .ToList();

            return courses;
        }

        #endregion Methods
    }
}