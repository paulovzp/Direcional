using Direcional.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Direcional.Persistence.Common;

public class DirecionalRepository<T> : IDirecionalRepository<T>
    where T : DirecionalEntity
{
    protected readonly DirecionalDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public DirecionalRepository(DirecionalDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task Add(T entity) => await _dbSet.AddAsync(entity);

    public void Dispose() => _dbContext.Dispose();

    public async Task<bool> Exists(Expression<Func<T, bool>> expression) 
        => await _dbSet.Where(expression).AnyAsync();

    public async Task<T?> Get(int id) => 
        await Get().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Tuple<IEnumerable<T>, int>> Get(Expression<Func<T, bool>> expression, Expression<Func<T, object>> sorting, bool reverse, int page, int pageSize)
    {
        var query = Get()
            .Where(expression);

        if (reverse)
            query = query.OrderByDescending(sorting);
        else
            query = query.OrderBy(sorting);

        int totalCount = await query.CountAsync();

        if (totalCount == 0)
            return new Tuple<IEnumerable<T>, int>(Enumerable.Empty<T>(), 0);

        var result = await query.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
        return new Tuple<IEnumerable<T>, int>(result.AsEnumerable(), totalCount);
    }

    protected virtual IQueryable<T> Get()
    {
        return _dbSet.AsQueryable();
    }

    public Task<List<T>> Get(Expression<Func<T, bool>> expression) 
        => Get().Where(expression).ToListAsync();

    public async Task Remove(T entity)
    {
        _dbSet.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }
}
