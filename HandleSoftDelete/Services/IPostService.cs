using HandleSoftDelete.Dtos;

namespace HandleSoftDelete.Services
{
    public interface IPostService
    {
        Task AddPostAsync(PostDto postDto);
        PostDto GetPost(int id);
        IEnumerable<PostDto> GetPosts(bool includeDeleted = false);
        Task HardDeletePost(int id);
        Task SoftDeletePost(int id);
        Task UpdatePost(PostDto postDto);
    }
}