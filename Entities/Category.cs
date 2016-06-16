using System.Collections.Generic;

namespace LMS.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        // Navigation properties
        public ICollection<PostCategory> PostCategories { get; set; }
    }
}