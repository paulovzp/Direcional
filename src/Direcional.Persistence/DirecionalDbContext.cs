using Direcional.Domain;
using Microsoft.EntityFrameworkCore;

namespace Direcional.Persistence;

public class DirecionalDbContext : DbContext
{
    public DirecionalDbContext(DbContextOptions<DirecionalDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;

        foreach (var entry in ChangeTracker.Entries()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified)))
        {
            if (entry.Entity is DirecionalEntity)
            {
                var entity = entry.Entity as DirecionalEntity;
                if (entry.State == EntityState.Added)
                    entity!.CreatedAt = now;

                entity!.UpdatedAt = now;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}