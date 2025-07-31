using Direcional.Domain;
using Direcional.Persistence.Common;

namespace Direcional.Persistence;

public class ReservaRepository : DirecionalRepository<Reserva>, IReservaRepository
{
    public ReservaRepository(DirecionalDbContext dbContext)
        : base(dbContext)
    {
    }
}
