using Direcional.Application.Common;
using Direcional.Domain;
using System.Linq.Expressions;

namespace Direcional.Application;

public class ApartamentoAppService :
    DirecionalAppService<Apartamento, ApartamentoResponse, ApartamentoReadResponse, ApartamentoCreateRequest, ApartamentoUpdateRequest, ApartamentoFilterRequest>
    , IApartamentoAppService
{
    private IApartamentoService _apartamentoService => (IApartamentoService)_service;
    public ApartamentoAppService(IApartamentoService service,
        IApartamentoRepository repository,
        IDirecionalUnitOfWork unitOfWork)
        : base(service, repository, unitOfWork)
    {
    }

    public override List<Expression<Func<Apartamento, bool>>> GetFilter(FilterRequest<ApartamentoFilterRequest> request)
    {
        var expressions = new List<Expression<Func<Apartamento, bool>>>();

        if (request.Filter is null)
            return expressions;

        if (!string.IsNullOrEmpty(request.Filter.Nome))
            expressions.Add(func => func.Nome.Contains(request.Filter.Nome));

        if (request.Filter.Numero.HasValue)
            expressions.Add(func => func.Numero == request.Filter.Numero.Value);

        if (request.Filter.Andar.HasValue)
            expressions.Add(func => func.Andar == request.Filter.Andar.Value);

        if (request.Filter.ValorVendaInicio.HasValue)
            expressions.Add(func => func.ValorVenda >= request.Filter.ValorVendaInicio.Value);

        if (request.Filter.ValorVendaFim.HasValue)
            expressions.Add(func => func.ValorVenda <= request.Filter.ValorVendaFim.Value);

        return expressions;
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
            Nome = request.Nome,
            Numero = request.Numero,
            Andar = request.Andar,
            ValorVenda = request.ValorVenda
        };
    }

    public override Apartamento ToEntity(ApartamentoUpdateRequest request, Apartamento apartamento)
    {
        apartamento.ValorVenda = request.ValorVenda;
        apartamento.Nome = request.Nome;
        return apartamento;
    }

    public async Task<ApartamentoDisponivel> Disponivel(int apartamentoId)
    {
        var apartamento = await ReadEntity(apartamentoId);
        var disponivel = await _apartamentoService.Disponivel(apartamento.Id);
        string baseMessage = $"Apartamento {apartamento.Nome} {apartamento.Numero} no andar {apartamento.Andar}";
        string message = disponivel ? $"{baseMessage} disponível." : $"{baseMessage} não disponível.";
        return new ApartamentoDisponivel(message);
    }

    public override IEnumerable<ApartamentoReadResponse> ToReadResponse(IEnumerable<Apartamento> entities)
    {
        return entities.Select(x => new ApartamentoReadResponse
        {
            Numero = x.Numero,
            Nome = x.Nome,
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
            ValorVenda = entity.ValorVenda,
            Nome = entity.Nome
        };
    }
}