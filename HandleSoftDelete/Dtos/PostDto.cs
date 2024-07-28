using HandleSoftDelete.Entities;
using System.ComponentModel.DataAnnotations;

namespace HandleSoftDelete.Dtos
{
    public class PostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
