using System.Linq.Expressions;

namespace Direcional.Domain;

public interface IDirecionalRepository<T> : IDisposable
    where T : DirecionalEntity
{
    Task<T?> Get(int id);
    Task<bool> Exists(Expression<Func<T, bool>> expression);
    Task<Tuple<IEnumerable<T>, int>> Get(Expression<Func<T, bool>> expression, Expression<Func<T, object>> sorting,
        bool reverse, int page, int pageSize);
    Task<List<T>> Get(Expression<Func<T, bool>> expression);
    Task Add(T entity);
    Task Update(T entity);
    Task Remove(T entity);
}
