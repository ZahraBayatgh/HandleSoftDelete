
namespace HandleSoftDelete.Dtos
{
    public class CommentDto 
    {
        public string Text { get; set; }

        public string Content { get;  set; }
        public int PostId { get;  set; }
        public DateTime CreatedAt { get;  set; }
    }
}
