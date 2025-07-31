using Direcional.Application.Common;

namespace Direcional.Application;

public interface IVendaAppService :
    IDirecionalAppService<VendaResponse, VendaReadResponse, VendaCreateRequest, VendaUpdateRequest, VendaFilterRequest>
{
}
