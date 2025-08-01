using Direcional.Infrastructure.Enums;

namespace Direcional.Domain;

public class ApartamentoService : DirecionalService<Apartamento>, IApartamentoService
{
    private readonly IVendaRepository _vendaRepository;
    private readonly IReservaRepository _reservaRepository;

    public ApartamentoService(IApartamentoRepository repository,
        IApartamentoValidator validator,
        IVendaRepository vendaRepository,
        IReservaRepository reservaRepository)
        : base(repository, validator)
    {
        _vendaRepository = vendaRepository;
        _reservaRepository = reservaRepository;
    }

    public async Task<bool> Disponivel(int apartamentoId)
    {
        return await Vendido(apartamentoId) || await Reservado(apartamentoId);
    }

    public async Task<bool> Vendido(int apartamentoId)
    {
        return await _vendaRepository.Exists(x => x.ApartamentoId == apartamentoId);
    }

    public async Task<bool> Reservado(int apartamentoId)
    {
        return await _reservaRepository.Exists(x => x.ApartamentoId == apartamentoId && x.Status != ReservaStatus.Cancelada && x.Status != ReservaStatus.Expirada);
    }

    public async Task<bool> Exists(int apartamentoId)
    {
        return await _repository.Exists(x => x.Id == apartamentoId);
    }

    public async Task MarcarVendido(Apartamento apartamento)
    {
        apartamento.MarcarVendido();
        await _repository.Update(apartamento);
    }
}
