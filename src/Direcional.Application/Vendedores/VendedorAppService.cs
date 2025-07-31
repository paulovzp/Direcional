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
            "nome" => func => func.Id,
            _ => func => func.Id,
        };
    }

    public override Vendedor ToEntity(VendedorCreateRequest request)
    {
        return new Vendedor();
    }

    public override Vendedor ToEntity(VendedorUpdateRequest request)
    {
        return new Vendedor();
    }

    public override IEnumerable<VendedorReadResponse> ToReadResponse(IEnumerable<Vendedor> entities)
    {
        return entities.Select(x => new VendedorReadResponse
        {

        });
    }

    public override VendedorResponse ToResponse(Vendedor entity)
    {
        return new VendedorResponse
        {
        };
    }
}