using Direcional.Domain;
using Direcional.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace Direcional.Persistence;

public class ReservaRepository : DirecionalRepository<Reserva>, IReservaRepository
{
    public ReservaRepository(DirecionalDbContext dbContext)
        : base(dbContext)
    {
    }

    protected override IQueryable<Reserva> Get()
    {
        return _dbSet
            .Include(x => x.Apartamento)
            .Include(x => x.Corretor)
            .Include(x => x.Cliente);
    }
}
