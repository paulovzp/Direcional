namespace Direcional.Domain;

public interface IDirecionalUnitOfWork : IDisposable
{
    Task<bool> CommitAsync();
}
