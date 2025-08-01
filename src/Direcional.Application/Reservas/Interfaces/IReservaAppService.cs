using Direcional.Application.Common;

namespace Direcional.Application;

public interface IReservaAppService :
    IDirecionalAppService<ReservaResponse, ReservaReadResponse, ReservaCreateRequest, ReservaUpdateRequest, ReservaFilterRequest>
{
    Task Cancelar(int id);
}
