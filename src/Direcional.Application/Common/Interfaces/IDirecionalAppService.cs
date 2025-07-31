namespace Direcional.Application.Common;

public interface IDirecionalAppService<TResponse, TReadResponse, TCreateRequest, TUpdateRequest, TFilterRequest>
{
    Task<int> Add(TCreateRequest request);
    Task Update(int id, TUpdateRequest request);
    Task Delete(int id);
    Task<TResponse> Read(int id);
    Task<PaginationResponse<TReadResponse>> Read(FilterRequest<TFilterRequest> request);
}

