using Direcional.Application.Common;

namespace Direcional.Application;

public interface ICorretorAppService :
    IDirecionalAppService<CorretorResponse, CorretorReadResponse, CorretorCreateRequest, CorretorUpdateRequest, CorretorFilterRequest>
{
}
