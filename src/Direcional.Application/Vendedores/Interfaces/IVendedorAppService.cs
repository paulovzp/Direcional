using Direcional.Application.Common;

namespace Direcional.Application;

public interface IVendedorAppService :
    IDirecionalAppService<VendedorResponse, VendedorReadResponse, VendedorCreateRequest, VendedorUpdateRequest, VendedorFilterRequest>
{
}
