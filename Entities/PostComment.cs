using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Entities
{
    public class PostComment
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }


        [ForeignKey("Comment")]
        public int CommentId { get; set; }
    }
}