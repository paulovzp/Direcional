using Direcional.Application.Common;
using Direcional.Domain;
using System.Linq.Expressions;

namespace Direcional.Application;

public class ReservaAppService :
    DirecionalAppService<Reserva, ReservaResponse, ReservaReadResponse, ReservaCreateRequest, ReservaUpdateRequest, ReservaFilterRequest>
    , IReservaAppService
{
    public ReservaAppService(IReservaService service,
        IReservaRepository repository,
        IDirecionalUnitOfWork unitOfWork)
        : base(service, repository, unitOfWork)
    {
    }

    public override Expression<Func<Reserva, bool>> GetFilter(FilterRequest<ReservaFilterRequest> request)
    {
        return x => true;
    }

    public override Expression<Func<Reserva, object>> GetSort(string sortBy)
    {
        return sortBy.ToLower() switch
        {
            "nome" => func => func.Id,
            _ => func => func.Id,
        };
    }

    public override Reserva ToEntity(ReservaCreateRequest request)
    {
        return new Reserva();
    }

    public override Reserva ToEntity(ReservaUpdateRequest request)
    {
        return new Reserva();
    }

    public override IEnumerable<ReservaReadResponse> ToReadResponse(IEnumerable<Reserva> entities)
    {
        return entities.Select(x => new ReservaReadResponse
        {

        });
    }

    public override ReservaResponse ToResponse(Reserva entity)
    {
        return new ReservaResponse
        {
        };
    }
}