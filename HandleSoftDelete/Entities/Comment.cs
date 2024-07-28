
using System.ComponentModel.DataAnnotations;

namespace HandleSoftDelete.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public int PostId { get; set; }

        // Navigation property
        public Post Post { get; set; }
        [Required]
        public string Content { get; internal set; }
    }

}
