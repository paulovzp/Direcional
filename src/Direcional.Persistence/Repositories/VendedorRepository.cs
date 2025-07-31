using Direcional.Domain;
using Direcional.Persistence.Common;

namespace Direcional.Persistence;

public class VendedorRepository : DirecionalRepository<Vendedor>, IVendedorRepository
{
    public VendedorRepository(DirecionalDbContext dbContext)
        : base(dbContext)
    {
    }
}
