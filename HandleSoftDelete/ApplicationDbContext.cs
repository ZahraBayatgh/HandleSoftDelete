using HandleSoftDelete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace HandleSoftDelete
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Cascade soft delete configuration
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Comment>().HasQueryFilter(c => !c.IsDeleted);

        }


        public override int SaveChanges()
        {

            var entities = ChangeTracker.Entries<BaseEntity>()
                 .Where(e => e.State == EntityState.Deleted)
                 .ToList();

            foreach (var entity in entities)
            {
                entity.State = EntityState.Modified;
                entity.Entity.IsDeleted = true;
                entity.Entity.DeletedAt = DateTime.UtcNow;
            }

            return base.SaveChanges();

        }
      
    }

}
