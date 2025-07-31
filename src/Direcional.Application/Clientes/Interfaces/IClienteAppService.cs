using Direcional.Application.Common;

namespace Direcional.Application;

public interface IClienteAppService :
    IDirecionalAppService<ClienteResponse, ClienteReadResponse, ClienteCreateRequest, ClienteUpdateRequest, ClienteFilterRequest>
{
}
