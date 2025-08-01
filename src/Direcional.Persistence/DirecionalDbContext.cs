using Direcional.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Direcional.Persistence;

public class DirecionalDbContext : DbContext
{
    public DirecionalDbContext(DbContextOptions<DirecionalDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DirecionalDbContext).Assembly);
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

public class DirecionalDbContextFactory : IDesignTimeDbContextFactory<DirecionalDbContext>
{
    public DirecionalDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DirecionalDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=DirecionalDB;User=sa;Password=d1r3c10nalDBp@ssw0rd;TrustServerCertificate=true;");
        
        return new DirecionalDbContext(optionsBuilder.Options);
    }
}