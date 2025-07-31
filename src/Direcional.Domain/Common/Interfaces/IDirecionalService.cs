namespace Direcional.Domain;

public interface IDirecionalService<T>
        where T : DirecionalEntity
{
    Task<T> Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task Delete(int id);
}
