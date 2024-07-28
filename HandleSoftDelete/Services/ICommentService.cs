using HandleSoftDelete.Dtos;

namespace HandleSoftDelete.Services
{
    public interface ICommentService
    {
        Task AddCommentAsync(CommentDto commentDto);
        CommentDto GetComment(int id);
        IEnumerable<CommentDto> GetComments(int postId, bool includeDeleted = false);
        Task HardDeleteCommentAsync(int id);
        Task SoftDeleteCommentAsync(int id);
        Task UpdateCommentAsync(int id, CommentDto commentDto);
    }
}