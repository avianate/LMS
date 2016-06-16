using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAllPublishedPosts()
        {
            throw new NotImplementedException();
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