using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Entities
{
    public class PostTag
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }


        [ForeignKey("Tag")]
        public int TagId { get; set; }
    }
}