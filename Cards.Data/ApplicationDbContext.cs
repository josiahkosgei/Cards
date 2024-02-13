using Cards.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cards.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions){}
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Card>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
