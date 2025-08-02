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
        Seed(modelBuilder);
    }

    private void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().HasData(new Usuario
        {
            Id = 1,
            Email = "admin@direcional.com.br",
            Nome = "Administrador",
            Tipo = Infrastructure.Enums.TipoUsuario.Admin,
            HashPassword = "6Q8lFNrM3ry1v/lnmq7SaK+eKp10mY+3Oq/3T8fcHy8=",
            Salt = "+jpUXXIe840y6q0avkrIyQ==",
            CreatedAt = new DateTime(2024, 08, 02, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2024, 08, 02, 0, 0, 0, DateTimeKind.Utc)
        });
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