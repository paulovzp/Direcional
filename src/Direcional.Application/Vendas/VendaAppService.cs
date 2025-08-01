using Direcional.Application.Common;
using Direcional.Domain;
using Direcional.Infrastructure.Exceptions;
using System.Linq.Expressions;

namespace Direcional.Application;

public class VendaAppService :
    DirecionalAppService<Venda, VendaResponse, VendaReadResponse, VendaCreateRequest, VendaUpdateRequest, VendaFilterRequest>
    , IVendaAppService
{
    private readonly IReservaRepository _reservaRepository;
    private IVendaService _vendaService => (IVendaService)_service;

    public VendaAppService(IVendaService service,
        IVendaRepository repository,
        IDirecionalUnitOfWork unitOfWork,
        IReservaRepository reservaRepository)
        : base(service, repository, unitOfWork)
    {
        _reservaRepository = reservaRepository;
    }

    public async Task Efetuar(int reservaId, PagamentoReservaRequest request)
    {
        var reserva = await _reservaRepository.Get(reservaId) ?? throw new DirecionalNotFoundException($"Reserva com id {reservaId} não encontrada.");
        await _vendaService.Efetuar(reserva, request.ValorEntrada);
        await _unitOfWork.CommitAsync();
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
            ApartamentoAndar = x.Apartamento.Andar,
            ApartamentoNome = x.Apartamento.Nome,
            ApartamentoId = x.Apartamento.Id,
            ApartamentoNumero = x.Apartamento.Numero,
            ClienteId = x.Cliente.Id,
            ClienteNome = x.Cliente.Nome,
            CorretorId = x.Corretor.Id,
            CorretorNome = x.Corretor.Nome,
            DataVenda = x.DataVenda,
            Id = x.Id,
            Valor = x.Valor,
            ValorEntrada = x.ValorEntrada
        });
    }

    public override VendaResponse ToResponse(Venda entity)
    {
        return new VendaResponse
        {
            ApartamentoAndar = entity.Apartamento.Andar,
            ApartamentoNome = entity.Apartamento.Nome,
            ApartamentoId = entity.Apartamento.Id,
            ApartamentoNumero = entity.Apartamento.Numero,
            ClienteId = entity.Cliente.Id,
            ClienteNome = entity.Cliente.Nome,
            CorretorId = entity.Corretor.Id,
            CorretorNome = entity.Corretor.Nome,
            DataVenda = entity.DataVenda,
            Id = entity.Id,
            Valor = entity.Valor,
            ValorEntrada = entity.ValorEntrada
        };
    }
}