using Direcional.Domain;
using Direcional.Persistence.Common;

namespace Direcional.Persistence;

public class UsuarioRepository : DirecionalRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(DirecionalDbContext dbContext)
        : base(dbContext)
    {
    }
}
