using HandleSoftDelete.Entities;

namespace HandleSoftDelete.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IQueryable<T> GetAll(bool includeDeleted = false);

        void Add(T entity);

        void Update(T entity);

        void SoftDelete(T entity);

        void HardDelete(T entity);
    }
    }