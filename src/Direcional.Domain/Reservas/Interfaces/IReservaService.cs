namespace Direcional.Domain;

public interface IReservaService : IDirecionalService<Reserva>
{
    Task Cancelar(Reserva reserva);
}

public interface IReservaValidator : IDirecionalValidator<Reserva>
{
}