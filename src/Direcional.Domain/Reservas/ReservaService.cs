using Direcional.Infrastructure.Enums;
using Direcional.Infrastructure.Exceptions;

namespace Direcional.Domain;

public class ReservaService : DirecionalService<Reserva>, IReservaService
{
    public ReservaService(IReservaRepository repository, IReservaValidator validator)
        : base(repository, validator)
    {
    }

    public async Task Cancelar(Reserva reserva)
    {

        if (reserva.Status != ReservaStatus.Pendente)
            throw new DirecionalDomainException($"Reserva não pode ser cancelar, status atual {reserva.Status.ToString()}");

        reserva.UpdateStatus(ReservaStatus.Cancelada);
        await _repository.Update(reserva);
    }
}
