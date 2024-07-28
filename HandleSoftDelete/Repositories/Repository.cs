using HandleSoftDelete.Entities;
using Microsoft.EntityFrameworkCore;

namespace HandleSoftDelete.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public T GetById(int id)
        {
            return _dbSet.AsNoTracking().SingleOrDefault(e => e.Id == id && !e.IsDeleted);
        }

        public IQueryable<T> GetAll(bool includeDeleted = false)
        {
            if (includeDeleted)
            {
                return _dbSet.AsNoTracking();
            }

            return _dbSet.AsNoTracking().Where(e => !e.IsDeleted);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void SoftDelete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void HardDelete(T entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }

}
