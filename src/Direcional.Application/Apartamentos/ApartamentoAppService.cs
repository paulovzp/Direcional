using Direcional.Application.Common;
using Direcional.Domain;
using System.Linq.Expressions;

namespace Direcional.Application;

public class ApartamentoAppService :
    DirecionalAppService<Apartamento, ApartamentoResponse, ApartamentoReadResponse, ApartamentoCreateRequest, ApartamentoUpdateRequest, ApartamentoFilterRequest>
    , IApartamentoAppService
{
    public ApartamentoAppService(IApartamentoService service,
        IApartamentoRepository repository, 
        IDirecionalUnitOfWork unitOfWork) 
        : base(service, repository, unitOfWork)
    {
    }

    public override Expression<Func<Apartamento, bool>> GetFilter(FilterRequest<ApartamentoFilterRequest> request)
    {
        return x => true; 
    }

    public override Expression<Func<Apartamento, object>> GetSort(string sortBy)
    {
        return sortBy.ToLower() switch
        {
            "nome" => func => func.Id,
            _ => func => func.Id,
        };
    }

    public override Apartamento ToEntity(ApartamentoCreateRequest request)
    {
        return new Apartamento();
    }

    public override Apartamento ToEntity(ApartamentoUpdateRequest request)
    {
        return new Apartamento();
    }

    public override IEnumerable<ApartamentoReadResponse> ToReadResponse(IEnumerable<Apartamento> entities)
    {
        return entities.Select(x => new ApartamentoReadResponse
        {

        });
    }

    public override ApartamentoResponse ToResponse(Apartamento entity)
    {
        return new ApartamentoResponse
        {
        };
    }
}