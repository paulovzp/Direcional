using Direcional.Application.Common;
using Direcional.Domain;
using System.Linq.Expressions;

namespace Direcional.Application;

public class ClienteAppService :
    DirecionalAppService<Cliente, ClienteResponse, ClienteReadResponse, ClienteCreateRequest, ClienteUpdateRequest, ClienteFilterRequest>
    , IClienteAppService
{
    public ClienteAppService(IClienteService service,
        IClienteRepository repository,
        IDirecionalUnitOfWork unitOfWork)
        : base(service, repository, unitOfWork)
    {
    }

    public override Expression<Func<Cliente, bool>> GetFilter(FilterRequest<ClienteFilterRequest> request)
    {
        return x => true;
    }

    public override Expression<Func<Cliente, object>> GetSort(string sortBy)
    {
        return sortBy.ToLower() switch
        {
            "nome" => func => func.Id,
            _ => func => func.Id,
        };
    }

    public override Cliente ToEntity(ClienteCreateRequest request)
    {
        return new Cliente();
    }

    public override Cliente ToEntity(ClienteUpdateRequest request)
    {
        return new Cliente();
    }

    public override IEnumerable<ClienteReadResponse> ToReadResponse(IEnumerable<Cliente> entities)
    {
        return entities.Select(x => new ClienteReadResponse
        {

        });
    }

    public override ClienteResponse ToResponse(Cliente entity)
    {
        return new ClienteResponse
        {
        };
    }
}