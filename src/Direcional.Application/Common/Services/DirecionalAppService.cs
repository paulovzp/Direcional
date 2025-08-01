using Direcional.Domain;
using Direcional.Infrastructure.Exceptions;
using System.Linq.Expressions;

namespace Direcional.Application.Common;

public abstract class DirecionalAppService<TEntity, TReponse, TReadResponse, TCreateRequest, TUpdateRequest, TFilterRequest>
    : IDirecionalAppService<TReponse, TReadResponse, TCreateRequest, TUpdateRequest, TFilterRequest>
    where TEntity : DirecionalEntity
{
    protected readonly IDirecionalService<TEntity> _service;
    protected readonly IDirecionalRepository<TEntity> _repository;
    protected readonly IDirecionalUnitOfWork _unitOfWork;

    public DirecionalAppService(IDirecionalService<TEntity> service,
        IDirecionalRepository<TEntity> repository,
        IDirecionalUnitOfWork unitOfWork)
    {
        _service = service;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


    public virtual async Task<int> Add(TCreateRequest request)
    {
        var entity = ToEntity(request);
        entity = await _service.Add(entity);
        await _unitOfWork.CommitAsync();
        return entity.Id;
    }

    public virtual async Task<TReponse> Read(int id)
    {
        var entity = await ReadEntity(id);
        return ToResponse(entity);
    }

    public virtual async Task<PaginationResponse<TReadResponse>> Read(FilterRequest<TFilterRequest> request)
    {
        var filter = GetFilter(request);
        var orderBy = GetSort(request.Ordenation.OrderBy);
        var result = await _repository.Get(filter, orderBy, request.Ordenation.IsDescending(), request.Page, request.PageSize);
        var entityMaping = ToReadResponse(result.Item1);
        var response = new PaginationResponse<TReadResponse>(entityMaping, result.Item2);
        return response;
    }

    public virtual async Task Delete(int id)
    {
        var entity = await ReadEntity(id);
        await _service.Delete(entity);
        await _unitOfWork.CommitAsync();
    }

    public virtual async Task Update(int id, TUpdateRequest request)
    {
        TEntity entity = await ReadEntity(id);
        entity = ToEntity(request, entity);
        await _service.Update(entity);
        await _unitOfWork.CommitAsync();
    }

    protected virtual async Task<TEntity> ReadEntity(int id)
    {
        var entity = await _repository.Get(id) ?? throw new DirecionalNotFoundException("Entity not found");

        return entity;
    }


    public abstract Expression<Func<TEntity, bool>> GetFilter(FilterRequest<TFilterRequest> request);
    public abstract Expression<Func<TEntity, object>> GetSort(string sortBy);
    public abstract TEntity ToEntity(TCreateRequest request);
    public abstract TEntity ToEntity(TUpdateRequest request, TEntity entity);
    public abstract IEnumerable<TReadResponse> ToReadResponse(IEnumerable<TEntity> entities);
    public abstract TReponse ToResponse(TEntity entity);
}

