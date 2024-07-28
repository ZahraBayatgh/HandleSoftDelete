using HandleSoftDelete.Dtos;
using HandleSoftDelete.Entities;
using HandleSoftDelete.Repositories;
using Microsoft.Extensions.Hosting;

namespace HandleSoftDelete.Services
{

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public PostDto GetPost(int id)
        {
            var post = _postRepository.GetById(id);
            var postDto = new PostDto { Content = post.Content, Title = post.Title };

            return postDto;
        }

        public IEnumerable<PostDto> GetPosts(bool includeDeleted = false)
        {
            var posts = _postRepository.GetAll(includeDeleted);
            var postDtos = new List<PostDto>();

            foreach (var post in posts)
            {
                var postDto = new PostDto { Content = post.Content, Title = post.Title };
                postDtos.Add(postDto);
            }
            return postDtos;
        }

        public async Task AddPostAsync(PostDto postDto)
        {
            var post = new Post { Content = postDto.Content, Title = postDto.Title };
            _postRepository.Add(post);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task UpdatePost(PostDto postDto)
        {
            var post = new Post { Content = postDto.Content, Title = postDto.Title };
            _postRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task SoftDeletePost(int id)
        {
            var post = _postRepository.GetPost(id);
            _postRepository.SoftDelete(post);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task HardDeletePost(int id)
        {
            var post = _postRepository.GetById(id);
            _postRepository.HardDelete(post);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
