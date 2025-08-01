using Direcional.Domain;
using Direcional.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace Direcional.Persistence;

public class UsuarioRepository : DirecionalRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(DirecionalDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Usuario?> ObterPorEmail(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
    }
}
