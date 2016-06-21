using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Entities
{
    public class PostCategory
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
    }
}