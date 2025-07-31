using Direcional.Application.Common;
using Direcional.Domain;
using System.Linq.Expressions;

namespace Direcional.Application;

public class VendedorAppService :
    DirecionalAppService<Vendedor, VendedorResponse, VendedorReadResponse, VendedorCreateRequest, VendedorUpdateRequest, VendedorFilterRequest>
    , IVendedorAppService
{
    public VendedorAppService(IVendedorService service,
        IVendedorRepository repository,
        IDirecionalUnitOfWork unitOfWork)
        : base(service, repository, unitOfWork)
    {
    }

    public override Expression<Func<Vendedor, bool>> GetFilter(FilterRequest<VendedorFilterRequest> request)
    {
        return x => true;
    }

    public override Expression<Func<Vendedor, object>> GetSort(string sortBy)
    {
        return sortBy.ToLower() switch
        {
            "nome" => func => func.Nome,
            "telefone" => func => func.Telefone,
            "email" => func => func.Email,
            "codigo" => func => func.Codigo,
            _ => func => func.Id,
        };
    }

    public override Vendedor ToEntity(VendedorCreateRequest request)
        => Vendedor.Create(request.Nome, request.Email, request.Telefone);

    public override Vendedor ToEntity(VendedorUpdateRequest request, Vendedor vendedor)
    {
        vendedor.Update(request.Nome, request.Telefone);
        return vendedor;
    }

    public override IEnumerable<VendedorReadResponse> ToReadResponse(IEnumerable<Vendedor> entities)
    {
        return entities.Select(x => new VendedorReadResponse
        {
            Telefone = x.Telefone,
            Nome = x.Nome,
            Codigo = x.Codigo,
            Email = x.Email,
            DataInicio = x.DataInicio,
            DataFim = x.DataFim,
            Id = x.Id
        });
    }

    public override VendedorResponse ToResponse(Vendedor entity)
    {
        return new VendedorResponse
        {
            Telefone = entity.Telefone,
            Nome = entity.Nome,
            Codigo = entity.Codigo,
            Email = entity.Email,
            DataInicio = entity.DataInicio,
            DataFim = entity.DataFim,
            Id = entity.Id
        };
    }
}