using System;
using System.Collections.Generic;
using System.Linq;
using LMS.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class LMSRepository : ILMSRepository
    {
        private LMSContext _context;

        public LMSRepository(LMSContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }

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

        public IEnumerable<Post> GetAllPosts()
        {
            var posts = _context.Posts
                                    .OrderByDescending(d => d.ModifiedDate)
                                    .ToList();

            SetAllPostEntities(posts);

            return posts;
        }

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

        public IEnumerable<Tag> GetAllTags()
        {
            throw new NotImplementedException();
        }

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
    }
}