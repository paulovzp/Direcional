using Direcional.Domain;
using Direcional.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace Direcional.Persistence;

public class VendaRepository : DirecionalRepository<Venda>, IVendaRepository
{
    public VendaRepository(DirecionalDbContext dbContext)
        : base(dbContext)
    {
    }

    protected override IQueryable<Venda> Get()
    {
        return _dbSet
            .Include(x=> x.Apartamento)
            .Include(x => x.Corretor)
            .Include(x => x.Cliente);
    }
}
