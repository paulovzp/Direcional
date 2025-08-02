using Direcional.Infrastructure.Constants;
using Direcional.Infrastructure.Exceptions;

namespace Direcional.Domain;

public class VendaService : DirecionalService<Venda>, IVendaService
{
    private readonly IApartamentoRepository _apartamentoRepository;
    private readonly IApartamentoService _apartamentoService;
    private readonly IReservaService _reservaService;

    public VendaService(IVendaRepository repository,
        IVendaValidator validator,
        IApartamentoRepository apartamentoRepository,
        IApartamentoService apartamentoService,
        IReservaService reservaService)
        : base(repository, validator)
    {
        _apartamentoRepository = apartamentoRepository;
        _apartamentoService = apartamentoService;
        _reservaService = reservaService;
    }

    public async Task Efetuar(Reserva reserva, decimal valorEntrada)
    {
        if(valorEntrada <= 0)
            throw new DirecionalDomainException(MensagemValidacao.Venda.ValorEntradaMenorZero);

        var apartamento = await _apartamentoRepository.Get(reserva.ApartamentoId);
        var venda = Venda.Create(reserva.ClienteId, reserva.ApartamentoId, apartamento!.ValorVenda, valorEntrada, reserva.CorretorId);
        await Add(venda);
        await _apartamentoService.MarcarVendido(apartamento!);
        await _reservaService.MarcarConfirmar(reserva);
    }
}
