namespace Direcional.Domain;

public class ReservaService : DirecionalService<Reserva>, IReservaService
{
    public ReservaService(IReservaRepository repository, IReservaValidator validator)
        : base(repository, validator)
    {
    }
}
