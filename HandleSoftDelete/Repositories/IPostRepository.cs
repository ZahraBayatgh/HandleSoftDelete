using HandleSoftDelete.Entities;

namespace HandleSoftDelete.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Post GetPost(int id);
    }
   }