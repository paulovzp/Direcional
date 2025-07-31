using Direcional.Domain;
using Direcional.Persistence.Common;

namespace Direcional.Persistence;

public class VendaRepository : DirecionalRepository<Venda>, IVendaRepository
{
    public VendaRepository(DirecionalDbContext dbContext)
        : base(dbContext)
    {
    }
}
