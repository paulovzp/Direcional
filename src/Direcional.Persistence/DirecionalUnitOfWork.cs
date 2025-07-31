using Direcional.Domain;

namespace Direcional.Persistence;

public class DirecionalUnitOfWork : IDirecionalUnitOfWork
{
    private readonly DirecionalDbContext _context;

    public DirecionalUnitOfWork(DirecionalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CommitAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
