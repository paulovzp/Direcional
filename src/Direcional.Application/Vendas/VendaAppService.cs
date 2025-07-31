using Direcional.Application.Common;
using Direcional.Domain;
using System.Linq.Expressions;

namespace Direcional.Application;

public class VendaAppService :
    DirecionalAppService<Venda, VendaResponse, VendaReadResponse, VendaCreateRequest, VendaUpdateRequest, VendaFilterRequest>
    , IVendaAppService
{
    public VendaAppService(IVendaService service,
        IVendaRepository repository,
        IDirecionalUnitOfWork unitOfWork)
        : base(service, repository, unitOfWork)
    {
    }

    public override Expression<Func<Venda, bool>> GetFilter(FilterRequest<VendaFilterRequest> request)
    {
        return x => true;
    }

    public override Expression<Func<Venda, object>> GetSort(string sortBy)
    {
        return sortBy.ToLower() switch
        {
            "nome" => func => func.Id,
            _ => func => func.Id,
        };
    }

    public override Venda ToEntity(VendaCreateRequest request)
    {
        return new Venda();
    }

    public override Venda ToEntity(VendaUpdateRequest request, Venda reserva)
    {
        return reserva;
    }

    public override IEnumerable<VendaReadResponse> ToReadResponse(IEnumerable<Venda> entities)
    {
        return entities.Select(x => new VendaReadResponse
        {

        });
    }

    public override VendaResponse ToResponse(Venda entity)
    {
        return new VendaResponse
        {
        };
    }
}