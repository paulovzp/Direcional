using Direcional.Domain;
using Direcional.Persistence.Common;

namespace Direcional.Persistence;

public class ClienteRepository : DirecionalRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(DirecionalDbContext dbContext)
        : base(dbContext)
    {
    }
}
