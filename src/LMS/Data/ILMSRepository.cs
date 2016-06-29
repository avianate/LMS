﻿using System.Collections.Generic;
using LMS.Entities;

namespace LMS.Data
{
    /// <summary>
    ///     LMSRepository interface
    /// </summary>
    public interface ILMSRepository
    {
        IEnumerable<Post> GetAllPosts();

        IEnumerable<Post> GetAllPublishedPosts();

        IEnumerable<Post> GetUserPosts(string name);

        IEnumerable<Comment> GetAllCommentsForPost(int postId);

        IEnumerable<Category> GetAllCategories();

        IEnumerable<Category> GetAllCategoriesForPost(int postId);

        IEnumerable<Tag> GetAllTags();

        IEnumerable<Tag> GetAllTagsForPost(int postId);

        IEnumerable<Course> GetAllCourses();

        IEnumerable<Course> GetAllPublishedCourses();
    }
}