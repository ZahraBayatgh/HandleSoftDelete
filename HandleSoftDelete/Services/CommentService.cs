using HandleSoftDelete.Dtos;
using HandleSoftDelete.Entities;
using HandleSoftDelete.Repositories;

namespace HandleSoftDelete.Services
{
    public class CommentService :  ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IRepository<Comment> commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public CommentDto GetComment(int id)
        {
            var comment = _commentRepository.GetById(id);
            var commentDto = new CommentDto
            {
                Content = comment.Content,
                PostId = comment.PostId,
                CreatedAt = comment.CreatedAt
            };

            return commentDto;
        }

        public IEnumerable<CommentDto> GetComments(int postId, bool includeDeleted = false)
        {
            var comments = _commentRepository.GetAll(includeDeleted)
                                             .Where(c => c.PostId == postId);
            var commentDtos = new List<CommentDto>();

            foreach (var comment in comments)
            {
                var commentDto = new CommentDto
                {
                    Content = comment.Content,
                    PostId = comment.PostId,
                    CreatedAt = comment.CreatedAt
                };
                commentDtos.Add(commentDto);
            }
            return commentDtos;
        }

        public async Task AddCommentAsync(CommentDto commentDto)
        {
            var comment = new Comment
            {
                Content = commentDto.Content,
                PostId = commentDto.PostId,
                CreatedAt = commentDto.CreatedAt,
                Text = commentDto.Text
            };
            _commentRepository.Add(comment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(int id, CommentDto commentDto)
        {
            var comment = new Comment
            {
                Id = id,
                Content = commentDto.Content,
                PostId = commentDto.PostId,
                CreatedAt = commentDto.CreatedAt
            };
            _commentRepository.Update(comment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task SoftDeleteCommentAsync(int id)
        {
            var comment = _commentRepository.GetById(id);
            _commentRepository.SoftDelete(comment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task HardDeleteCommentAsync(int id)
        {
            var comment = _commentRepository.GetById(id);
            _commentRepository.HardDelete(comment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
