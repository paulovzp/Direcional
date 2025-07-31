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
        Expression<Func<Cliente, bool>> expression = x => true;
        return expression;
    }

    public override Expression<Func<Cliente, object>> GetSort(string sortBy)
    {
        return sortBy.ToLower() switch
        {
            "nome" => func => func.Nome,
            "email" => func => func.Email,
            _ => func => func.Id,
        };
    }

    public override Cliente ToEntity(ClienteCreateRequest request)
        => Cliente.Create(request.Nome, request.Email, request.Telefone);

    public override Cliente ToEntity(ClienteUpdateRequest request, Cliente cliente)
    {
        cliente.Update(request.Nome, request.Telefone);
        return cliente;
    }

    public override IEnumerable<ClienteReadResponse> ToReadResponse(IEnumerable<Cliente> entities)
    {
        return entities.Select(x => new ClienteReadResponse
        {
            Id = x.Id,
            Nome = x.Nome,
            Email = x.Email,
            Telefone = x.Telefone
        });
    }

    public override ClienteResponse ToResponse(Cliente entity)
    {
        return new ClienteResponse
        {
            Id = entity.Id,
            Nome = entity.Nome,
            Email = entity.Email,
            Telefone = entity.Telefone
        };
    }
}