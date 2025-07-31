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
            "andar" => func => func.Andar,
            "numero" => func => func.Numero,
            _ => func => func.Id,
        };
    }

    public override Apartamento ToEntity(ApartamentoCreateRequest request)
    {
        return new Apartamento()
        {
            Numero = request.Numero,
            Andar = request.Andar,
            ValorVenda = request.ValorVenda
        };
    }

    public override Apartamento ToEntity(ApartamentoUpdateRequest request, Apartamento apartamento)
    {
        apartamento.ValorVenda = request.ValorVenda;
        return apartamento;
    }

    public override IEnumerable<ApartamentoReadResponse> ToReadResponse(IEnumerable<Apartamento> entities)
    {
        return entities.Select(x => new ApartamentoReadResponse
        {
            Numero = x.Numero,
            Andar = x.Andar,
            Id = x.Id,
            ValorVenda = x.ValorVenda
        });
    }

    public override ApartamentoResponse ToResponse(Apartamento entity)
    {
        return new ApartamentoResponse
        {
            Numero = entity.Numero,
            Andar = entity.Andar,
            Id = entity.Id,
            ValorVenda = entity.ValorVenda
        };
    }
}