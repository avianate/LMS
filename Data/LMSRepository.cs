using System;
using System.Collections.Generic;
using System.Linq;
using LMS.Entities;

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
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetAllCommentsForPost(int postId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            var posts = _context.Posts.OrderBy(t => t.Title).ToList();

            return posts;
        }

        public IEnumerable<Post> GetAllPublishedPosts()
        {
            var posts = _context.Posts
                                    .Where(p => p.IsPublished)
                                    .OrderBy(t => t.Title)
                                    .ToList();

            return posts;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetAllTagsForPost(int postId)
        {
            throw new NotImplementedException();
        }
    }
}