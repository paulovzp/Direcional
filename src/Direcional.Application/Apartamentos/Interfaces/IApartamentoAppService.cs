using Azure;
using Direcional.Application.Common;

namespace Direcional.Application;

public interface IApartamentoAppService :
    IDirecionalAppService<ApartamentoResponse, ApartamentoReadResponse, ApartamentoCreateRequest, ApartamentoUpdateRequest, ApartamentoFilterRequest>
{
}
