using Direcional.Domain;
using Direcional.Persistence.Common;

namespace Direcional.Persistence;

public class ApartamentoRepository : DirecionalRepository<Apartamento>, IApartamentoRepository
{
    public ApartamentoRepository(DirecionalDbContext dbContext) 
        : base(dbContext)
    {
    }
}
