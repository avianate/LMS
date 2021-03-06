using System.Collections.Generic;

namespace LMS.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        public string TagName { get; set; }

        // Navigation properties
        public ICollection<PostTag> PostTags { get; set; }
    }
}