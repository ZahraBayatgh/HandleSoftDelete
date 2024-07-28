using HandleSoftDelete.Entities;
using Microsoft.EntityFrameworkCore;

namespace HandleSoftDelete.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Post? GetPost(int id)
        {
          return _context.Posts
                      .Include(p => p.Comments)
                      .FirstOrDefault(p => p.Id == id);
        }
    }

}
