using Direcional.Domain;
using Direcional.Persistence.Common;

namespace Direcional.Persistence;

public class CorretorRepository : DirecionalRepository<Corretor>, ICorretorRepository
{
    public CorretorRepository(DirecionalDbContext dbContext)
        : base(dbContext)
    {
    }
}
